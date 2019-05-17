﻿using FirstApp.Core.Interfaces;
using FirstApp.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace FirstApp.Core.ViewModels
{
    public class TaskListViewModel : BaseViewModel, IListHandler
    {
        public int taskItem;
        private readonly IDBTaskService _dBTaskService;

        public TaskListViewModel(IMvxNavigationService navigationService, IDBTaskService dBTaskService) : base(navigationService)
        {
            _dBTaskService = dBTaskService;
            DeleteItemCommand = new MvxCommand<int>(RemoveCollectionItem);
            DeleteItemCommandiOS = new MvxCommand<int>(RemoveCollectionItemiOS);
            AddData();
            ShowTaskChangedView = new MvxAsyncCommand<TaskModel>(CollectionItemClick);
        }
        public IMvxCommand<TaskModel> ShowTaskChangedView { get; set; }

        public void AddData()
        {
            IsRefreshTaskCollection = true;

            var list = _dBTaskService.LoadListItemsTask();

            foreach (var item in list)
            {
                item.VmHandler = this;
            }

            TaskCollection = new MvxObservableCollection<TaskModel>();
            TaskCollection.AddRange(list);

            IsRefreshTaskCollection = false;
        }

        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            MvxObservableCollection<TaskModel> obsSender = sender as MvxObservableCollection<TaskModel>;

            int element = e.OldItems.Count;
        }

        private bool _isRefreshTaskCollection;
        public bool IsRefreshTaskCollection
        {
            get => _isRefreshTaskCollection;
            set
            {
                _isRefreshTaskCollection = value;
                RaisePropertyChanged(() => IsRefreshTaskCollection);
            }
        }

        private MvxCommand _refreshCommand;
        public MvxCommand RefreshTaskCommand => _refreshCommand = _refreshCommand ?? new MvxCommand(AddData);

        private MvxObservableCollection<TaskModel> _taskCollection;
        public MvxObservableCollection<TaskModel> TaskCollection
        {
            get => _taskCollection;
            set
            {
                _taskCollection = value;
                RaisePropertyChanged(() => TaskCollection);
            }
        }

        public MvxAsyncCommand CreateNewTask
        {
            get
            {
                return new MvxAsyncCommand(async () =>
                {
                    await _navigationService.Navigate<TaskDetailsViewModel>();
                });
            }
        }

        public IMvxCommand<int> ItemClickCommand { get; set; }

        public IMvxCommand<int> DeleteItemCommand { get; set; }

        public IMvxCommand<int> DeleteItemCommandiOS { get; set; }

        public override void ViewAppearing()
        {
            RefreshTaskCommand.Execute();
        }

        public async Task CollectionItemClick(TaskModel model)
        {
            await _navigationService.Navigate<TaskDetailsViewModel, TaskModel>(model);
        }

        public void RemoveCollectionItem(int itemId)
        {
            TaskModel itemForDelete = null;
            _dBTaskService.DeleteTaskFromTable(itemId);
            foreach (TaskModel item in TaskCollection)
            {
                if (item.Id == itemId)
                {
                    itemForDelete = item;
                }
            }

            TaskCollection.Remove(item: itemForDelete);
        }

        public void RemoveCollectionItemiOS(int itemId)
        {
            var idForDB = TaskCollection[itemId].Id;
            TaskCollection.RemoveAt(itemId);
            _dBTaskService.DeleteTaskFromTable(idForDB);
        }
    }
}
