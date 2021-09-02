using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Практика01._07._21
{
    class Program
    {
        static void Main(string[] args)
        {
            //отправили запрос 
            WebRequest request = WebRequest.Create("http://www.szse.cn/api/report/ShowReport/data?SHOWTYPE=JSON&CATALOGID=1956&loading=first&random=0.5933660096413333");
            string text;
            // Получили данные 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
            {
                // Записываем json строку в text
                char[] MyChar = { ']' };
                text = sr.ReadToEnd().TrimEnd(MyChar); ;
            }
            for (int n=2; n<51; n++)
            {
                WebRequest request_n = WebRequest.Create("http://www.szse.cn/api/report/ShowReport/data?SHOWTYPE=JSON&CATALOGID=1956&TABKEY=tab1&PAGENO=" +n+ "&random=");
                HttpWebResponse response_n = (HttpWebResponse)request_n.GetResponse();
                using (StreamReader sr = new StreamReader(response_n.GetResponseStream()))
                {
                    // Записываем json строку в text
                    text += ",";
                    text += sr.ReadToEnd().Trim(new Char[] { '[', ']' });
                }
            }
            text += "]";
            
            //Десериализуем json строку
            List<RootObject> result = JsonConvert.DeserializeObject<List<RootObject>>(text);
            // Создаем строимтель строк
            
            var csv = new StringBuilder();
            foreach (RootObject i in result)
            {
                foreach (Class1 j in i.data)
                {
                    //Удаляем из строк запятые
                    //j.sys_key = j.amplitude.Replace(",", "");
                    int first_sys_key = j.sys_key.IndexOf("<u>") + "<u>".Length;
                    int last_sys_key = j.sys_key.LastIndexOf("</u>");
                    int first_jjywjc = j.jjywjc.IndexOf("<u>") + "<u>".Length;
                    int last_jjywjc = j.jjywjc.LastIndexOf("</u>");

                    var sys_keyOut = j.sys_key.Substring(first_sys_key, last_sys_key - first_sys_key);
                    var jjywjcOut = j.jjywjc.Substring(first_jjywjc, last_jjywjc - first_jjywjc); ;
                    var ssrqOut = j.ssrq;
                    var dqgmOut = j.dqgm;
                    //Задаем формат строки
                    var newLine = string.Format("{0};{1};{2};{3}", sys_keyOut, jjywjcOut, ssrqOut, dqgmOut);
                    csv.AppendLine(newLine);
                    Console.WriteLine(j.sys_key.Substring(first_sys_key, last_sys_key - first_sys_key));
                    Console.WriteLine(j.jjywjc);
                    Console.WriteLine(j.ssrq);
                    Console.WriteLine(j.dqgm);
                    
                }
            }
            //Создаем файл csv и записываем туда данные
            File.WriteAllText("Таблица.csv", csv.ToString());
            Console.ReadLine();
        }
    }
}
