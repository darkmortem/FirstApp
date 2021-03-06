using CoreGraphics;
using FirstApp.Core;
using FirstApp.Core.ViewModels;
using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Plugin.FilePicker;
using Plugin.FilePicker.Abstractions;
using System;
using UIKit;

namespace FirstApp.iOS.ViewControllers.Tasks
{
    public partial class TaskDetailsViewController : MvxViewController<TaskDetailsViewModel>
    {
        #region Variables

        private UIButton _backButton;
        private FileTVS _source;

        #endregion Variables

        #region Constructors

        public TaskDetailsViewController() : base(nameof(TaskDetailsViewController), null)
        {
            Title = Constants.TaskDetails;
        }

        #endregion Constructors

        #region Methods

        public async void OpenFile(object sender, EventArgs e)
        {
            string fileName = null;

            FileData fileData = await CrossFilePicker.Current.PickFile();

            if (fileData == null)
            {
                return;
            }

            fileName = fileData.FileName;

            if (!string.IsNullOrEmpty(fileName))
            {
                ViewModel.SaveFileName(fileName);
            }
        }

        private void SetBind()
        {
            MvxFluentBindingDescriptionSet<TaskDetailsViewController, TaskDetailsViewModel> set = this.CreateBindingSet<TaskDetailsViewController, TaskDetailsViewModel>();

            set.Bind(TaskName).To(vm => vm.TaskName);
            set.Bind(_source).To(vm => vm.FileNameList);
            set.Bind(_source).For(s => s.DeleteRowCommandiOS).To(vm => vm.DeleteFileItemCommand);
            set.Bind(TaskDescription).To(vm => vm.TaskDescription);
            set.Bind(AddMapMarkersButton).To(vm => vm.AddMarkerCommand);
            set.Bind(DeleteTaskButton).To(vm => vm.DeleteTask);
            set.Bind(MapMarkersCount).To(vm => vm.MarkersCounter);
            set.Bind(SaveTaskButton).To(vm => vm.SaveTask);

            set.Apply();
        }

        private void SetupNavigationBar()
        {
            _backButton = new UIButton(UIButtonType.Custom);
            _backButton.Frame = new CGRect(0, 0, 40, 40);
            _backButton.SetImage(UIImage.FromBundle("backButton"), UIControlState.Normal);

            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { new UIBarButtonItem(_backButton) }, false);

            MvxFluentBindingDescriptionSet<TaskDetailsViewController, TaskDetailsViewModel> set = this.CreateBindingSet<TaskDetailsViewController, TaskDetailsViewModel>();
            set.Bind(_backButton).To(vm => vm.BackViewCommand);
            set.Apply();
        }

        #endregion Methods

        #region Overrides

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _source = new FileTVS(FileTabelView);

            FileTabelView.Source = _source;

            SetBind();

            SetupNavigationBar();

            TaskName.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            TaskDescription.ShouldReturn = (textField) =>
            {
                textField.ResignFirstResponder();
                return true;
            };

            FileTabelView.RegisterNibForCellReuse(FileItemCellViewController.FileNib, FileItemCellViewController.FileKey);

            AddFileInTaskButton.TouchUpInside += OpenFile;
        }

        public override void ViewDidUnload()
        {
            AddFileInTaskButton.TouchUpInside -= OpenFile;

            base.ViewDidUnload();
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            View.EndEditing(true);

            base.TouchesBegan(touches, evt);
        }

        public override void ViewWillAppear(bool animated)
        {
            ViewModel.UpdateMarkersCounter();

            TabBarController.TabBar.Hidden = true;

            base.ViewWillAppear(animated);
        }

        #endregion Overrides
    }
}