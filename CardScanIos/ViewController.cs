using System;
using ScanCardBinding;
using UIKit;

namespace CardScanIos
{
    public partial class ViewController : UIViewController
    {


        public ViewController(IntPtr handle) : base(handle)
        {
        }



        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.


        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            var ver = UIDevice.CurrentDevice.SystemVersion;

            var vc = ScanViewController.CreateViewControllerWithDelegate(new Junk());
            var junk = ScanViewController.IsCompatible;
            PresentViewController(vc, animated: true, completionHandler: null);
        }


    }

    internal class Junk : ScanDelegate
    {
        public override void UserDidCancel(ScanViewController scanViewController)
        {
            throw new NotImplementedException();
        }

        public override void UserDidScanCard(ScanViewController scanViewController, CreditCard creditCard)
        {
            throw new NotImplementedException();
        }

        public override void UserDidSkip(ScanViewController scanViewController)
        {
            throw new NotImplementedException();
        }
    }
}