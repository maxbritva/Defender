using Save;
using Zenject;

namespace MainMenu
{
    public class MainMenuManager: IInitializable
    {
        [Inject]private DataProvider _dataProvider;

        public MainMenuManager()
        {
            //_dataProvider = dataProvider;
            
        }

        public void Initialize()
        {
            //  _dataProvider.TryLoad();
        }
    }
}