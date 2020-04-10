namespace Lab2.Entities
{
    public class ChangedObject
    {
        public string AttributeName { get; }
        public object PreviousValue { get; }
        public object Value { get; }

        public ChangedObject(string attributeName, object previousValue, object value)
        {
            AttributeName = attributeName;
            PreviousValue = previousValue;
            Value = value;
        }
    }
}