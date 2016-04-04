using System.Collections.Generic;

namespace JsonSplitter.Core
{
    public class Splitter
    {
        public Splitter(string sourceFile, string folderDestination,
            bool generateOneObjFile,
            int maxItemInFile, out int generateItems, out int processObjects)
        {
            var list = FileJsonHelper.DeserializeSequenceFromJson<dynamic>(sourceFile);

            var addItemList = new List<dynamic>();
            generateItems = 0;
            processObjects = 0;
            foreach (var item in list)
            {
                ++processObjects;
                item.id = "";
                addItemList.Add(item);
                if (addItemList.Count == maxItemInFile)
                {
                    ++generateItems;
                    if (maxItemInFile == 1 && generateOneObjFile)
                    {
                        FileJsonHelper.SerializeSequenceToJson<dynamic>(item, $"{folderDestination}{generateItems}.json");
                    }
                    else
                    {
                        FileJsonHelper.SerializeSequenceToJsonList<dynamic>(addItemList, $"{folderDestination}{generateItems}.json");
                    }
                    addItemList = new List<dynamic>();
                   
                }
               
            }
        }
    }
}
