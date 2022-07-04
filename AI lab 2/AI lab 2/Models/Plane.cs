using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AI_lab_2.Models
{
    public class Plane : ICloneable
    {
        public (int, int) size { get; set; }
        public List<Field> fields { get; set; }
        public List<RowColumn> rowsAndColumns { get; set; }

        static public List<RowColumn> SplitFieldsIntoRows(Plane plane) 
        {
            List<RowColumn> answer = new List<RowColumn>();

            for (int i = 0; i < plane.size.Item1; i++)
                answer.Add(new RowColumn { fields = plane.fields.FindAll(o => o.coordinates.Item1 == i) });

            for (int i = 0; i < plane.size.Item2; i++)
                answer.Add(new RowColumn { fields = plane.fields.FindAll(o => o.coordinates.Item2 == i) });

            foreach (RowColumn i in answer)
            {
                i.fields.Sort((o1, o2) => o1.coordinates.Item1 - o2.coordinates.Item1);
                i.fields.Sort((o1, o2) => o1.coordinates.Item2 - o2.coordinates.Item2);
            }

            return answer;
        }

        public object Clone()
        {
            Plane clone = new Plane();
            clone.size = size;

            List<Field> cloneFields = new List<Field>();

            foreach (Field i in this.fields) 
            {
                cloneFields.Add(new Field 
                {
                    coordinates = i.coordinates,
                    value = i.value,
                    possibleValues = new List<int>(i.possibleValues)
                });
            }

            foreach (Field i in this.fields.FindAll(o => o.greaterThan != null))
            {
                Field buffer = cloneFields.Find(o => o.coordinates == i.coordinates);
                foreach (Field j in i.greaterThan) 
                {
                    if(buffer.greaterThan == null)
                        buffer.greaterThan = new List<Field>();
                    buffer.greaterThan.Add(cloneFields.Find(o => o.coordinates == j.coordinates));

                }
            }

            clone.fields = cloneFields;
            clone.rowsAndColumns = Plane.SplitFieldsIntoRows(clone);

            return clone;
        }

        public override string ToString()
        {
            StringBuilder answerBuild = new StringBuilder();
            answerBuild.Append(size + "\n");

            for (int i = 0; i < size.Item1; i++)
            {
                for (int j = 0; j < size.Item2; j++)
                {
                    Field buffer = fields.Find(o => o.coordinates == (j, i));
                    string strbuf = buffer.value == int.MinValue ? "X" : buffer.value.ToString();
                    answerBuild.Append(String.Format("{0, 4:N0}", strbuf));
                }
                answerBuild.Append("\n");
            }

            foreach (Field i in fields.FindAll(o => o.greaterThan != null)) 
            {
                answerBuild.Append(String.Format("{0, 4:N0:}", i));
                foreach (Field j in i.greaterThan)
                {
                    answerBuild.Append(String.Format("{0, 4:N0,}", j));
                }
                answerBuild.Append("\n");
            }

            return answerBuild.ToString();
        }

        public string ToStringDisplay()
        {
            StringBuilder answerBuild = new StringBuilder();
            answerBuild.Append(size + "\n");

            for (int i = 0; i < size.Item1; i++)
            {
                for (int j = 0; j < size.Item2; j++)
                {
                    Field buffer = fields.Find(o => o.coordinates == (j, i));
                    string strbuf = buffer.value == int.MinValue ? "X" : buffer.value.ToString();
                    answerBuild.Append(String.Format("{0, 4:N0}", strbuf));
                }
                answerBuild.Append("\n");
            }

            return answerBuild.ToString();
        }

    }

    public class RowColumn 
    {
        public List<Field> fields { get; set; }
        public int getEntropy() 
        {
            int answer = 1;
            foreach (Field i in fields)
                if (i.value == int.MinValue)
                    answer *= i.possibleValues.Count;
            return answer;
        }

        public bool isComplete() 
        {
            return !fields.Any(o => o.value == int.MinValue);
        }

        public static bool operator ==(RowColumn rc1, RowColumn rc2)
        {
            if (rc1.fields.Count != rc2.fields.Count)
                return false;

            for (int i = 0; i < rc1.fields.Count; i++) 
            {
                if (rc1.fields[i].value != rc2.fields[i].value)
                    return false;
            }

            return true;
        }

        public static bool operator !=(RowColumn rc1, RowColumn rc2)
        {
            return !(rc1 == rc2);
        }

        public override string ToString()
        {
            return String.Format("Size: {0}, Entropy: {1}", fields.Count, getEntropy());
        }
    }
}
