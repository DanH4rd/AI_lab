using System;
using System.Collections.Generic;
using System.Text;
using AI_lab.Models;

using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace AI_lab.Logic
{

    static class ReadData 
    {
        static public List<Flow> readFlows(string fileName) 
        {
            string jsonString = "";
            try
            {
                jsonString = File.ReadAllText(fileName);
            }
            catch (Exception e) 
            {
                Console.WriteLine("ReadException source: {0}", e.Source);
            }

            List<Flow> connections = JsonSerializer.Deserialize<List<Flow>>(jsonString);
            return connections;
        }

        static public List<Cost> readCosts(string fileName)
        {
            string jsonString = "";
            try
            {
                jsonString = File.ReadAllText(fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("ReadException source: {0}", e.Source);
            }

            List<Cost> connections = JsonSerializer.Deserialize<List<Cost>>(jsonString);
            return connections;
        }
    }

    public interface GetData
    {
        public int getDeviceCount();
        public (int, int) getGridSize();
        public List<Cost> getCosts();
        public List<Flow> getFlows();

    }

    public class EasyData : GetData
    {
        string dataFolderPath;
        public EasyData(string dataFolderPath) 
        {
            this.dataFolderPath = dataFolderPath;
        }

        public List<Cost> getCosts()
        {
            return ReadData.readCosts(dataFolderPath + @"\easy_cost.json");
        }

        public List<Flow> getFlows()
        {
            return ReadData.readFlows(dataFolderPath + @"\easy_flow.json");
        }

        public int getDeviceCount()
        {
            return 9;
        }

        public (int, int) getGridSize()
        {
            return (3,3);
        }
    }

    public class FlatData : GetData
    {
        string dataFolderPath;
        public FlatData(string dataFolderPath)
        {
            this.dataFolderPath = dataFolderPath;
        }

        public List<Cost> getCosts()
        {
            return ReadData.readCosts(dataFolderPath + @"\flat_cost.json");
        }

        public List<Flow> getFlows()
        {
            return ReadData.readFlows(dataFolderPath + @"\flat_flow.json");
        }

        public int getDeviceCount()
        {
            return 12;
        }

        public (int, int) getGridSize()
        {
            return (1, 12);
        }
    }

    public class HardData : GetData
    {
        string dataFolderPath;
        public HardData(string dataFolderPath)
        {
            this.dataFolderPath = dataFolderPath;
        }

        public List<Cost> getCosts()
        {
            return ReadData.readCosts(dataFolderPath + @"\hard_cost.json");
        }

        public List<Flow> getFlows()
        {
            return ReadData.readFlows(dataFolderPath + @"\hard_flow.json");
        }

        public int getDeviceCount()
        {
            return 24;
        }

        public (int, int) getGridSize()
        {
            return (5, 6);
        }
    }
}
