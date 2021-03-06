﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DLLSpecial;
using System.IO;

namespace MIDPS_Lab5
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
                        if (pageName != "user")
                        {
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
                        else
                        {
                            if (model.currentUser.isAdmin)
                            {
                                model.currentPage = pageName;
                                view.hideList(true);
                                view.setData1(Singleton.Instance.getUserList());
                            }
                        }
                    }
                    break;
                case Notification.RowSelected:
                    {
                        if (model.currentUser.canViewExtra)
                        {
                            if (!model.shouldHideList(model.currentPage))
                            {
                                int id = (int)data.GetValue(0);
                                model.selectedID = id;
                                view.setData2(Singleton.Instance.getData2(model.typeFromString(model.currentPage), id));
                            }
                            if (model.shouldShowImage())
                            {
                                int id = (int)data.GetValue(0);
                                model.selectedID = id;
                                view.showPicture(Singleton.Instance.getImage(id));
                            }
                        }
                    }
                    break;
                case Notification.DeleteRow:
                    {
                        if (model.currentUser.canEdit)
                        {
                            int id = (int)data.GetValue(0);
                            Singleton.Instance.deleteOne(model.typeFromString(model.currentPage), id);
                            view.setData1(Singleton.Instance.getData(model.typeFromString(model.currentPage)));
                        }
                    }
                    break;
                case Notification.DeleteMultipleRows:
                    {
                        if (model.currentUser.canEdit)
                        {
                            int nr = (int)data.GetValue(0);
                            Singleton.Instance.deleteMultiple(model.typeFromString(model.currentPage), nr);
                            view.setData1(Singleton.Instance.getData(model.typeFromString(model.currentPage)));
                        }
                    }
                    break;
                case Notification.UpdateRowOk:
                    {
                        if (model.currentUser.canEdit)
                        {
                            int id = (int)data.GetValue(0);
                            string newName = (string)data.GetValue(1);
                            Singleton.Instance.update(model.typeFromString(model.currentPage), id, newName);
                            view.setData1(Singleton.Instance.getData(model.typeFromString(model.currentPage)));
                        }
                    }
                    break;
                case Notification.UpdateImage:
                    {
                        if (model.currentUser.canEdit)
                        {
                            int id = (int)data.GetValue(0);
                            string filePath = (string)data.GetValue(1);
                            FileInfo info = new FileInfo(filePath);
                            byte[] dataBytes = new byte[info.Length];
                            FileStream fs = new FileStream(filePath, FileMode.Open,
                                      FileAccess.Read, FileShare.Read);
                            fs.Read(dataBytes, 0, (int)info.Length);
                            fs.Close();
                            Singleton.Instance.updateImage(id, dataBytes);
                        }
                    }
                    break;

                case Notification.SaveXML:
                    {
                        Singleton.Instance.saveToXML();
                    }
                    break;
                case Notification.LoadXML:
                    {
                        Singleton.Instance.loadFromXML((bool) data.GetValue(0));
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
                        Dictionary<string, object> temp = (Dictionary<string, object>) data.GetValue(0);
                        AddNewModel mdl = model.addNewConfig();
                        Singleton.Instance.Insert(mdl.buildObject(temp), model.currentPage);
                    }
                    break;

            }
        }
    }

    public class ModalControllerUpdate : Controller
    {
        public DialogUpdate view { get; set; }
        override public void OnNotification(string eventPath, Object target, params object[] data)
        {
        }
    }
}
