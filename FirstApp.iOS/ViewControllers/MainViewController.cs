﻿using FirstApp.Core.ViewModels;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;

namespace FirstApp.iOS.ViewControllers
{
    [MvxRootPresentation]
    public partial class MainViewController : MvxTabBarViewController<MainViewModel>
    {
        #region Variables

        private bool _isFirstTimePresented;

        #endregion Variables

        #region Constructors

        public MainViewController()
        {
            _isFirstTimePresented = true;
        }

        #endregion Constructors

        #region Overrides

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }

        public override void ViewWillAppear(bool animated)
        {
            if (_isFirstTimePresented)
            {
                _isFirstTimePresented = false;

                ViewModel.ShowMainViewCommand.Execute(null);
                ViewModel.ShowUserProfileViewModelCommand.Execute(null);
            }
        }

        #endregion Overrides
    }
}