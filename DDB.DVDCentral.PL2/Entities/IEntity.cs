namespace DDB.DVDCentral.PL2.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
        public string SortField { get; }

    }
}
