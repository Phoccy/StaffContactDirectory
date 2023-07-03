namespace StaffContactDirectory.View
{
    public partial class ContactListPage : ContentPage
    {
        private Task getPeople;
        public ContactListPage(ContactListViewModel viewModel)
        {
            InitializeComponent();
            getPeople = viewModel.GetPeople();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await getPeople;            
        }
    }
}
