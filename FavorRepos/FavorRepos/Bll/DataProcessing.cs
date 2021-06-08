using FavorRepos.Dal;
using FavorRepos.Model;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace FavorRepos.Bll
{
    public class DataProcessing
    {
        /// <summary>
        /// 获取Html信息
        /// </summary>
        public string GetHtmlInformation()
        {
            DBHelpCloud.ExecuteCommand($"delete tbl_Left");
            DBHelpCloud.ExecuteCommand($"delete tbl_Rigth");

            List<LibraryInformation> LLI = new List<LibraryInformation>();
            string url = "https://github.com/idcf-boat-house";
            var web = new HtmlWeb();
            var doc = web.Load(url);
            HtmlNodeCollection uiListNodes = doc.DocumentNode.SelectNodes("//div[@class='d-flex flex-justify-between']");
            foreach (var Nodes in uiListNodes)
            {
                LibraryInformation LI = new LibraryInformation();
                HtmlNode titleNode = Nodes.SelectSingleNode("div/h3/a");
                if (titleNode != null)
                {
                    LI.Name = titleNode.InnerText.Trim();
                    HtmlAttribute content_att = titleNode.Attributes["href"];
                    LI.Url = content_att.Value;
                }
                HtmlNode titleNodeOW = Nodes.SelectSingleNode("div/p/a");
                if (titleNodeOW != null)
                {
                    LI.OfficialWebsite = titleNodeOW.InnerText;
                }
                HtmlNode titleNodeDL = Nodes.SelectSingleNode("div/p/a[2]");
                if (titleNodeDL != null)
                {
                    LI.DocumentLibrary = titleNodeDL.InnerText;
                }
                LLI.Add(LI);

                if (!DBHelpCloud.Check_exists($"select Name from [tbl_Left] where Name = '{LI.Name}'"))
                {
                    DBHelpCloud.ExecuteCommand("INSERT INTO [dbo].[tbl_Left]([TNO],[Name],[OfficialWebsite],[DocumentLibrary],[Url]) VALUES" +
                                     $"(RIGHT(CONCAT('000000', FLOOR(RAND() * 1000000)), 6), '{LI.Name}', '{LI.OfficialWebsite}', '{LI.DocumentLibrary}','{LI.Url}')");
                }
            }

            string json = JsonConvert.SerializeObject(LLI);
            return json;
        }

        /// <summary>
        /// 添加收藏
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string AddRight(string Name)
        {

            try
            {
                if (!DBHelpCloud.Check_exists($"select Name from [tbl_Rigth] where Name = '{Name}'"))
                {
                    DBHelpCloud.ExecuteCommand("INSERT INTO [dbo].[tbl_Rigth]([TNO],[Name],[OfficialWebsite],[DocumentLibrary],[Url]) VALUES" +
                                                   $"select RIGHT(CONCAT('000000', FLOOR(RAND() * 1000000)), 6),Name,OfficialWebsite,DocumentLibrary,Url from [tbl_Left] where Name = '{Name}'");


                    DBHelpCloud.ExecuteCommand($"delete tbl_Left where Name = '{Name}'");
                }
                return "true";
            }
            catch (Exception)
            {
                return "false";
            }

        }

        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public string DeleteRight(string Name)
        {
            try
            {
                if (DBHelpCloud.Check_exists($"select Name from [tbl_Rigth] where Name = '{Name}'"))
                {
                    DBHelpCloud.ExecuteCommand("INSERT INTO [dbo].[tbl_Left]([TNO],[Name],[OfficialWebsite],[DocumentLibrary],[Url]) VALUES" +
                                                      $"select RIGHT(CONCAT('000000', FLOOR(RAND() * 1000000)), 6),Name,OfficialWebsite,DocumentLibrary,Url from tbl_Rigth where Name = '{Name}'");


                    DBHelpCloud.ExecuteCommand($"delete tbl_Rigth where Name = '{Name}'");
                }
                return "true";
            }
            catch (Exception)
            {
                return "false";
            }
        }

        public string GetRight()
        {
            try
            {
                List<LibraryInformation> LLI = new List<LibraryInformation>();
                DataTable DT = DBHelpCloud.GetDataTable(@"SELECT [Name]
                                                 ,[OfficialWebsite]
                                                 ,[DocumentLibrary]
                                                 ,[Url]
                                             FROM [MyFavorRepos].[dbo].[tbl_Rigth] ");

                foreach (DataRow item in DT.Rows)
                {
                    LibraryInformation Li = new LibraryInformation();
                    Li.Name = item["Name"].ToString();
                    Li.OfficialWebsite = item["OfficialWebsite"].ToString();
                    Li.DocumentLibrary = item["DocumentLibrary"].ToString();
                    Li.Url = item["Url"].ToString();
                    LLI.Add(Li);
                }
                string json = JsonConvert.SerializeObject(LLI);
                return json;
            }
            catch (Exception)
            {
                return "false";
            }
        }

        public string GetLeft()
        {
            try
            {
                List<LibraryInformation> LLI = new List<LibraryInformation>();
                DataTable DT = DBHelpCloud.GetDataTable(@"SELECT [Name]
                                                 ,[OfficialWebsite]
                                                 ,[DocumentLibrary]
                                                 ,[Url]
                                             FROM [MyFavorRepos].[dbo].[tbl_Left] ");

                foreach (DataRow item in DT.Rows)
                {
                    LibraryInformation Li = new LibraryInformation();
                    Li.Name = item["Name"].ToString();
                    Li.OfficialWebsite = item["OfficialWebsite"].ToString();
                    Li.DocumentLibrary = item["DocumentLibrary"].ToString();
                    Li.Url = item["Url"].ToString();
                    LLI.Add(Li);
                }
                string json = JsonConvert.SerializeObject(LLI);
                return json;
            }
            catch (Exception)
            {
                return "false";
            }
        }
    }
}
