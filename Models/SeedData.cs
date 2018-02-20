using KrisnaldoApp.Data;
using KrisnaldoApp.XMLMatch;
using KrisnaldoApp.XMLAlbumInfo;
using KrisnaldoApp.XMLSponsorInfo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace KrisnaldoApp.Models
{
    public class SeedData
    {

        public static int Initialize(IServiceProvider serviceProvider, IHostingEnvironment env)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                //Perform database delete and create, do not do this dangerous commands in production
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                //context.Database.Migrate();

                //LOCAL
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();

                return InitializeBis(context, env);
            }
        }

        public static int InitializeBis(ApplicationDbContext context, IHostingEnvironment env)
        {

            //SMARTERASP
            DeleteAllData(context);
            ImportMatches(context, env);
            ImportAlbums(context, env);
            ImportSponsors(context, env);

            int nmbOfChangesToTheUnderlyingDb = context.SaveChanges();
            context.Dispose();
            return nmbOfChangesToTheUnderlyingDb;

        }

        public static void DeleteAllData(ApplicationDbContext context)
        {
            string[] saTableArray = { "Goal", "SpelerMatch", "Paragraaf", "Match", "Speler", "Album", "Foto", "Seizoen", "Sponsor" };
            List<string> lsFullTableNames = new List<string>();
            foreach (string s in saTableArray)
            {
                //SMARTERASP
                context.Database.ExecuteSqlCommand("DELETE FROM [DB_A0576A_JoachimAlly9].[dbo].[" + s + "]");
            }
        }
        public static void ImportMatches(ApplicationDbContext context, IHostingEnvironment env)
        {
            string webRootPath = env.WebRootPath;
            string[] saFiles = Directory.GetFiles(webRootPath + @"\seed\matches\", "*.*", SearchOption.AllDirectories);
            List<string> lsAllPlayers = new List<string>();
            List<KrisnaldoApp.XMLMatch.Match> lmAllXMLMatches = new List<XMLMatch.Match>();
            foreach (string path in saFiles)
            {
                if (path.Contains(".xml"))
                {
                    try
                    {
                        lmAllXMLMatches.Add(KrisnaldoApp.XMLMatch.Match.LoadFromFile(path));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Exception occured: " + e.ToString());
                    }
                }
            }

            context.SaveChanges();


            int[] iaSeizoenStartJaren = new int[] { 2014, 2015, 2016, 2017 };
            for (int i = 1; i <= iaSeizoenStartJaren.Length; i++)
            {
                Models.Seizoen newSeizoen = new Seizoen();
                newSeizoen.SeizoenID = i;
                string sDitJaar = iaSeizoenStartJaren[i - 1] + "";
                string sVolgendJaar = ((int)iaSeizoenStartJaren[i - 1] + 1) + "";
                newSeizoen.SeizoenNaam = sDitJaar + "-" + sVolgendJaar;
                newSeizoen.StartDatum = new DateTime(iaSeizoenStartJaren[i - 1], 7, 1);
                newSeizoen.EindDatum = new DateTime(iaSeizoenStartJaren[i - 1] + 1, 6, 30);
                context.Seizoen.Add(newSeizoen);
            }


            int iTempNewSpelerID = 1;
            Dictionary<string, int> dAllPlayers = new Dictionary<string, int>();
            int iTempNewMatchID = 1;
            int iParagraafID = 1;
            int iCurrentSpelerID;
            foreach (XMLMatch.Match xmlMatch in lmAllXMLMatches)
            {
                Models.Match newMatch = new Match();
                newMatch.MatchID = iTempNewMatchID;
                newMatch.Datum = new DateTime(Convert.ToInt32(xmlMatch.Time.Year), Convert.ToInt32(xmlMatch.Time.Month), Convert.ToInt32(xmlMatch.Time.Day));
                newMatch.Tegenstander = xmlMatch.Against.TeamName;
                newMatch.SeizoenID = GiveCorrectSeizoenID(newMatch);
                switch (xmlMatch.Against.type)
                {
                    case "competition":
                        newMatch.Type = Type.COMPETITIE;
                        break;
                    case "friendly":
                        newMatch.Type = Type.VRIENDSCHAPPELIJK;
                        break;
                    case "cup":
                        newMatch.Type = Type.BEKER;
                        break;
                    case "tournament":
                        newMatch.Type = Type.TORNOOI;
                        break;
                    default: break;
                }
                switch (xmlMatch.Against.role)
                {
                    case "home":
                        newMatch.Rol = Rol.UIT;
                        newMatch.OnzeScore = Convert.ToInt32(xmlMatch.Score.Away);
                        newMatch.HunScore = Convert.ToInt32(xmlMatch.Score.Home);
                        break;
                    case "away":
                        newMatch.OnzeScore = Convert.ToInt32(xmlMatch.Score.Home);
                        newMatch.HunScore = Convert.ToInt32(xmlMatch.Score.Away);
                        newMatch.Rol = Rol.THUIS;
                        break;
                    default: break;
                }
                foreach (string sParagraph in xmlMatch.Report)
                {
                    Models.Paragraaf newParagraaf = new Paragraaf();
                    newParagraaf.ParagraafID = iParagraafID;
                    newParagraaf.MatchID = iTempNewMatchID;
                    newParagraaf.Tekst = sParagraph;
                    context.Paragraaf.Add(newParagraaf);
                    iParagraafID++;
                }
                context.Match.Add(newMatch);
                List<MatchPlayer> Players = xmlMatch.Players;
                foreach (MatchPlayer p in Players)
                {
                    string FullName = p.Value;
                    if (!dAllPlayers.ContainsKey(FullName))
                    {
                        dAllPlayers.Add(FullName, iTempNewSpelerID);
                        iCurrentSpelerID = iTempNewSpelerID;
                        iTempNewSpelerID++;
                    }
                    else
                    {
                        iCurrentSpelerID = dAllPlayers[FullName];
                    }
                    SpelerMatch newSpelerMatch = new SpelerMatch();
                    newSpelerMatch.SpelerID = iCurrentSpelerID;
                    newSpelerMatch.MatchID = iTempNewMatchID;
                    context.SpelerMatch.Add(newSpelerMatch);
                }
                iTempNewMatchID++;
            }
            //Add Goals
            iTempNewMatchID = 1;
            int iTempGoalID = 1;
            foreach (XMLMatch.Match xmlMatch in lmAllXMLMatches)
            {
                List<XMLMatch.MatchGoal> xmlGoals = xmlMatch.Goals;
                foreach (MatchGoal xmlGoal in xmlGoals)
                {
                    Goal newGoal = new Models.Goal();
                    newGoal.GoalID = iTempGoalID;
                    newGoal.MatchID = iTempNewMatchID;
                    if (dAllPlayers.ContainsKey(xmlGoal.Scored))
                    {
                        newGoal.ScorerSpelerID = dAllPlayers[xmlGoal.Scored];
                    }
                    if ((!String.IsNullOrWhiteSpace(xmlGoal.Assist)) && dAllPlayers.ContainsKey(xmlGoal.Assist))
                    {
                        newGoal.AssistSpelerID = dAllPlayers[xmlGoal.Assist];
                        newGoal.HasAssist = true;
                    }
                    else
                    {
                        newGoal.HasAssist = false;
                    }
                    context.Goal.Add(newGoal);
                    iTempGoalID++;
                }
                iTempNewMatchID++;
            }
            //Add Players
            foreach (KeyValuePair<string, int> entry in dAllPlayers)
            {
                int iPlaceOfFirstSpace = entry.Key.IndexOf(' ');
                context.Speler.Add(
                    new Speler
                    {
                        SpelerID = entry.Value,
                        VoorNaam = entry.Key.Substring(0, iPlaceOfFirstSpace),
                        FamilieNaam = entry.Key.Substring(iPlaceOfFirstSpace + 1)
                    }
                );
            }

        }

        public static void ImportAlbums(ApplicationDbContext context, IHostingEnvironment env)
        {
            string webRootPath = env.WebRootPath;
            AlbumInfo albumInfo = AlbumInfo.LoadFromFile(webRootPath + @"\seed\albums\albuminfo.xml");
            AlbumInfoAlbum[] albumArray = albumInfo.Albums;
            int iTempNewAlbumID = 1;
            int iTempFotoID = 1;
            foreach (AlbumInfoAlbum a in albumArray)
            {
                Console.WriteLine(a.DisplayName);
                Models.Album newAlbum = new Album();
                newAlbum.AlbumID = iTempNewAlbumID;
                newAlbum.Datum = new DateTime(a.Time.Year, a.Time.Month, a.Time.Day);
                newAlbum.DisplayNaam = a.DisplayName;
                newAlbum.Intro = a.Intro;
                newAlbum.RuweNaam = a.RawName;
                string[] saFiles = Directory.GetFiles(webRootPath + @"\seed\albums\" + a.RawName + @"\", "*.jpg");
                foreach (string path in saFiles)
                {
                    Models.Foto newFoto = new Foto();
                    newFoto.FotoID = iTempFotoID;
                    newFoto.AlbumID = iTempNewAlbumID;
                    //h:\root\home\joachimally - 001\www\site1\wwwroot\seed\albums\kwisnaldos2015\64.jpg
                    newFoto.RuweBestandsNaam = path.Substring(path.IndexOf("albums\\") + 7);
                    context.Foto.Add(newFoto);
                    iTempFotoID++;
                }
                iTempNewAlbumID++;
                context.Album.Add(newAlbum);
            }
        }
        public static void ImportSponsors(ApplicationDbContext context, IHostingEnvironment env)
        {
            //SponsorInfo SponsorInfo = SponsorInfo.LoadFromFile(webRootPath + @"\seed\sponsors\sponsorinfo.xml");
            //SponsorInfoSponsor[] SponsorArray = SponsorInfo.Sponsors;
            //foreach (SponsorInfoSponsor s in SponsorArray)
            //{
            //    Console.WriteLine(s.DisplayName);
            //    Models.Sponsor newSponsor = new Sponsor();
            //    newSponsor.DisplayNaam = s.DisplayName;
            //    newSponsor.Link = s.Link;
            //    newSponsor.RuwePictureNaam = s.RawPictureName;
            //    context.Sponsor.Add(newSponsor);
            //}


            string webRootPath = env.WebRootPath;
            //Read the contents of CSV file.  
            string csvData = File.ReadAllText(webRootPath + @"\seed\sponsors\sponsorinfo.csv");

            //Execute a loop over the rows.  
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    string[] csvRow = row.Split(',');
                    Models.Sponsor newSponsor = new Sponsor();
                    newSponsor.DisplayNaam = csvRow[0];
                    newSponsor.RuwePictureNaam = csvRow[1];
                    newSponsor.Link = csvRow[2];
                    context.Sponsor.Add(newSponsor);
                }
            }
        }



        public static string GiveFirstName(MatchPlayer player)
        {
            int iPlaceOfFirstSpace = player.Value.IndexOf(' ');
            return player.Value.Substring(0, iPlaceOfFirstSpace);
        }
        public static string GiveLastName(MatchPlayer player)
        {
            int iPlaceOfFirstSpace = player.Value.IndexOf(' ');
            return player.Value.Substring(iPlaceOfFirstSpace + 1);
        }

        public static int GiveCorrectSeizoenID(Match match)
        {
            int[] iaSeizoenStartJaren = new int[] { 2014, 2015, 2016, 2017 };
            int iRetValue = 0;
            for (int i = 1; i < iaSeizoenStartJaren.Length + 1; i++)
            {
                DateTime StartDatum = new DateTime(iaSeizoenStartJaren[i - 1], 7, 1);
                DateTime EindDatum = new DateTime(iaSeizoenStartJaren[i - 1] + 1, 6, 30);
                if (DateTime.Compare(StartDatum, match.Datum) < 0 && DateTime.Compare(match.Datum, EindDatum) < 0)
                {
                    iRetValue = i;
                }
            }
            return iRetValue;

        }
    }
}
