using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnirAsistentModel.Models;

// * load the text file
// * convert the text to List<NagradaModel>
// find the max ID
// Add the new record with the new ID (max + 1)
// Convert the prizes to list<string>
// Save the list<string> to the text file

namespace TurnirAsistentModel.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName) 
        {
            return $"{ ConfigurationManager.AppSettings["filePath"] }\\{ fileName }";
        }

        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file) == false)
            {
                return new List<string>();
            }

            return File.ReadAllLines(file).ToList();
        }
        public static List<NagradaModel> ConvertToNagradaModels(this List<string> lines)
        {
            List<NagradaModel> output = new List<NagradaModel>();
            foreach (string line in lines)
            {
                string[] cols = line.Split(',');

                NagradaModel p = new NagradaModel();
                p.ID = int.Parse(cols[0]);
                p.Mjesto = int.Parse(cols[1]);
                p.NazivMjesta = cols[2];
                p.IznosNagrade = decimal.Parse(cols[3]);
                p.PostotakNagrade = double.Parse(cols[4]);
                output.Add(p);
            }

            return output;
        }
        public static void SaveToPrizeFile(this List<NagradaModel> models, string fileName)
        {
            List<string> lines = new List<string>();

            foreach (NagradaModel p in models)
            {
                lines.Add($"{ p.ID },{ p.Mjesto },{ p.NazivMjesta },{ p.IznosNagrade },{ p.PostotakNagrade }");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
        
    }
}
