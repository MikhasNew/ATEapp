namespace ATEapp
{
    class IndividualClient : Client
    {
        public string Name { get; }
        public string LastName { get; }

        public IndividualClient(int id, string name, string lastName) : base(id)
        {
            Name = name;
            LastName = lastName;
        }
        
    }
}