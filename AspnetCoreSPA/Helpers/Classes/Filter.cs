namespace AspnetCoreSPATemplate.Helpers.Classes
{
    public class Filter
    {
        public string PropertyName { get; set; }
        public string Value { get; set; }
        public Enums.RelationalOperator RelationalOperator { get; set; }
        public Enums.LogicalOperator LogicalOperator { get; set; }
    }
}
