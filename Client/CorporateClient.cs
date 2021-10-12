namespace ATEapp
{
    class CorporateClient : Client
    {
        public string CompanyName { get; }
        public string Adress { get; }

        public CorporateClient(int id, string companyName, string adress) : base(id)
        {
            CompanyName = companyName;
            Adress = adress;
        }
        
    }
}