using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DLLSpecial;
namespace MIDPS_Lab4
{
    public abstract class Controller
    {
        public Model model { get; set; }
        abstract public void OnNotification(string eventPath, Object target, params object[] data);
        public Controller()
        {
        }
    }

    public class MainController : Controller
    {
        public MainWindow view { get; set; }
        public MainController(MainWindow view)
        {
            this.model = new Model();
            model.myController = this;
            this.view = view;
        }

        override public void OnNotification(string eventPath, Object target, params object[] data)
        {
            switch (eventPath)
            {
                case Notification.PageChange:
                    {
                        string pageName = (String)data.GetValue(0);
                        model.currentPage = pageName;
                        if (model.shouldHideList(pageName))
                        {
                            view.hideList(true);
                        }
                        else
                        {
                            view.hideList(false);
                            view.setData2(Singleton.Instance.getData2(model.typeFromString(pageName), 0));
                        }
                        view.setData1(Singleton.Instance.getData(model.typeFromString(pageName)));
                    }
                    break;
                case Notification.RowSelected:
                    {
                        if (!model.shouldHideList(model.currentPage))
                        {
                            int id = (int)data.GetValue(0);
                            model.selectedID = id;
                            view.setData2(Singleton.Instance.getData2(model.typeFromString(model.currentPage), id));
                        }
                    }
                    break;
            }

        }
    }

    public class ModalController : Controller
    {
        public DialogAdd view { get; set; }
        override public void OnNotification(string eventPath, Object target, params object[] data)
        {
            switch (eventPath)
            {
                case Notification.AddNewOK:
                    {
                        Dictionary<string, string> temp = (Dictionary<string, string>) data.GetValue(0);
                        AddNewModel mdl = model.addNewConfig();
                        Singleton.Instance.Insert(mdl.buildObject(temp));
                    }
                    break;

            }
        }


    }

}
