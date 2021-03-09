using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
namespace TaskPad
{
    public class ConfSettings
    {
        public string Username { get; set; }
        public string PathToFile { get; set; }

    }
    class Settings
    {
        private string ConfPath;
        private string Username;
        private string PathToFile;
        public Settings()
        {
            this.ConfPath = AppDomain.CurrentDomain.BaseDirectory + "RunConf.json";
            this.Username = Environment.UserName;
            this.PathToFile = GetPathFromFile();
        }

        //Used for start configuration
        public void StartConf()
        {
            if (IsUserNew() || IsPathEmpty())
            {
                PathSelect ps = new PathSelect();
                AddPerson();
                ps.Show();
            }



            Parser p = new Parser(this.PathToFile);

            p.fromJson();



            
        }


        //Deserializes json string
        public List<ConfSettings> DeserializeSettings(string raw)
        {
            Console.WriteLine(raw);
            List<ConfSettings> ConfList = JsonConvert.DeserializeObject<List<ConfSettings>>(raw);

            return ConfList;
        }

        //Serializes the list and returns Json as string
        public string SerializeSettings(List<ConfSettings> ConfList)
        {
            string serialized = JsonConvert.SerializeObject(ConfList);

            return serialized;
        }


        public string ReadConfFile()
        {
            if (!File.Exists(this.ConfPath))
            {
                WriteConfFile("[]");
            }

            return @File.ReadAllText(this.ConfPath);

        }

        //Used to write to RunConf file
        public void WriteConfFile(string raw)
        {
            try
            {
                File.WriteAllText(this.ConfPath, raw);
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR WRITING FILE!!!");
                Console.WriteLine(e.StackTrace);
            }

            

        }

        public Boolean IsUserNew()
        {
           List<ConfSettings> ConfList = DeserializeSettings(ReadConfFile());
            
            foreach(ConfSettings cf in ConfList)
            {
                if (cf.Username.Equals(this.Username))
                {
                    return false;
                }
            }

            return true;
        }

        public Boolean IsPathEmpty()
        {
            List<ConfSettings> ConfList = DeserializeSettings(ReadConfFile());

            foreach (ConfSettings cf in ConfList)
            {
                if (cf.Username.Equals(this.Username))
                {
                    if (cf.PathToFile.Equals(""))
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        //Adds new person to ConfList
        public void AddPerson()
        {
            List<ConfSettings> ConfList = DeserializeSettings(ReadConfFile());

            if (IsUserNew())
            {
                ConfSettings confsetting = new ConfSettings
                {
                    Username = this.Username,
                    PathToFile = ""
                };

                ConfList.Add(confsetting);
                WriteConfFile(SerializeSettings(ConfList));
            }
            
        }

        //changes or adds the users taskfile path
        public void AddPath(string path)
        {

            this.PathToFile = path;
            List<ConfSettings> ConfList = DeserializeSettings(ReadConfFile());

            foreach (ConfSettings cf in ConfList)
            {
                if (cf.Username.Equals(this.Username))
                {
                    cf.PathToFile = @path;
                }
            }
            WriteConfFile(SerializeSettings(ConfList));
            Parser p = new Parser(this.PathToFile);
            p.fromJson();
        }

        public void readconf()
        {
            List<ConfSettings> ConfList = DeserializeSettings(ReadConfFile());
            foreach (ConfSettings cf in ConfList)
            {
                Console.WriteLine(cf.PathToFile);
            }


        }

        public string GetPath()
        {
            Console.WriteLine(this.PathToFile);
            return this.PathToFile;
        }

        public string GetPathFromFile()
        {
            List<ConfSettings> ConfList = DeserializeSettings(ReadConfFile());
            string tmp = "";
            foreach (ConfSettings cf in ConfList)
            {
                if (cf.Username.Equals(this.Username))
                {
                    tmp = cf.PathToFile;
                }
            }
            return tmp;
        }


    }
}
