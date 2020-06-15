using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurnirAsistentModel.Models;
using TurnirAsistentModel.DataAccess.TextHelpers;

namespace TurnirAsistentModel.DataAccess
{
    public class TextConnector : IDataConnection
    {
        private const string NagradaFile = "NagradaModels.csv";

        public NagradaModel NapraviNagradu(NagradaModel model)
        {
            // load the text file convert the text to List<NagradaModel>
            List<NagradaModel> nagrade = NagradaFile.FullFilePath().LoadFile().ConvertToNagradaModels();
            //find the max ID
            int currentId = 1;

            if (nagrade.Count > 0)
            {
                currentId = nagrade.OrderByDescending(x => x.ID).First().ID + 1; 
            }
            model.ID = currentId;
            // Add the new record with the new ID (max + 1)
            nagrade.Add(model);


            // Convert the prizes to list<string>
            // Save the list<string> to the text file
            nagrade.SaveToPrizeFile(NagradaFile);

            return model;
        }
    }
}
