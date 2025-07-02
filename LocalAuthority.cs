namespace Casus4
{
    public class LocalAuthority
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LocalAuthority(int? id, string name)
        {
            Id = id ?? 0;
            Name = name;
        }

        public void Add() 
        {
            throw new NotImplementedException();
        }

        public void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
