using System;
using AVFoundation;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace ScanCardBinding
{
    [Static]
    //[Verify(ConstantsInterfaceAssociation)]
    partial interface Constants
    {
        // extern double CardScanVersionNumber;
        [Field("CardScanVersionNumber", "__Internal")]
        double CardScanVersionNumber { get; }

        // extern const unsigned char [] CardScanVersionString;
        //[Field("CardScanVersionString", "__Internal")]
        //byte[] CardScanVersionString { get; }
    }

    // @interface CornerView : UIView
    [Protocol]
    [BaseType(typeof(UIView))]
    interface CornerView
    {
        // -(instancetype _Nonnull)initWithFrame:(CGRect)frame __attribute__((objc_designated_initializer));
        [Export("initWithFrame:")]
        [DesignatedInitializer]
        IntPtr Constructor(CGRect frame);

        // -(instancetype _Nullable)initWithCoder:(NSCoder * _Nonnull)aDecoder __attribute__((objc_designated_initializer));
        //[Export("initWithCoder:")]
        //[DesignatedInitializer]
        //IntPtr Constructor(NSCoder aDecoder);
    }

    // @interface CreditCard : NSObject
    [Protocol]
    [BaseType(typeof(NSObject))]
    [DisableDefaultCtor]
    interface CreditCard
    {
        // @property (copy, nonatomic) NSString * _Nonnull number;
        [Export("number")]
        string Number { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable expiryMonth;
        [NullAllowed, Export("expiryMonth")]
        string ExpiryMonth { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable expiryYear;
        [NullAllowed, Export("expiryYear")]
        string ExpiryYear { get; set; }

        // @property (copy, nonatomic) NSString * _Nullable name;
        [NullAllowed, Export("name")]
        string Name { get; set; }

        // @property (nonatomic, strong) UIImage * _Nullable image;
        [NullAllowed, Export("image", ArgumentSemantic.Strong)]
        UIImage Image { get; set; }

        // +(instancetype _Nonnull)new __attribute__((deprecated("-init is unavailable")));
        [Static]
        [Export("new")]
        CreditCard New();
    }

    // @protocol ScanDelegate
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface ScanDelegate
    {
        // @required -(void)userDidCancel:(ScanViewController * _Nonnull)scanViewController;
        [Abstract]
        [Export("userDidCancel:")]
        void UserDidCancel(ScanViewController scanViewController);

        // @required -(void)userDidScanCard:(ScanViewController * _Nonnull)scanViewController creditCard:(CreditCard * _Nonnull)creditCard;
        [Abstract]
        [Export("userDidScanCard:creditCard:")]
        void UserDidScanCard(ScanViewController scanViewController, CreditCard creditCard);

        // @optional -(void)userDidScanQrCode:(ScanViewController * _Nonnull)scanViewController payload:(NSString * _Nonnull)payload;
        [Export("userDidScanQrCode:payload:")]
        void UserDidScanQrCode(ScanViewController scanViewController, string payload);

        // @required -(void)userDidSkip:(ScanViewController * _Nonnull)scanViewController;
        [Abstract]
        [Export("userDidSkip:")]
        void UserDidSkip(ScanViewController scanViewController);
    }

    // @protocol ScanStringsDataSource
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface ScanStringsDataSource
    {
        // @required -(NSString * _Nonnull)scanCard __attribute__((warn_unused_result));
        [Abstract]
        [Export("scanCard")]
        //[Verify(MethodToProperty)]
        string ScanCard { get; }

        // @required -(NSString * _Nonnull)positionCard __attribute__((warn_unused_result));
        [Abstract]
        [Export("positionCard")]
        //[Verify(MethodToProperty)]
        string PositionCard { get; }

        // @required -(NSString * _Nonnull)backButton __attribute__((warn_unused_result));
        [Abstract]
        [Export("backButton")]
        //[Verify(MethodToProperty)]
        string BackButton { get; }

        // @required -(NSString * _Nonnull)skipButton __attribute__((warn_unused_result));
        [Abstract]
        [Export("skipButton")]
        //[Verify(MethodToProperty)]
        string SkipButton { get; }
    }

    // @interface ScanViewController : UIViewController <AVCaptureVideoDataOutputSampleBufferDelegate>
    [Protocol]
    [BaseType(typeof(UIViewController))]
    interface ScanViewController : IAVCaptureVideoDataOutputSampleBufferDelegate
    {
        // @property (nonatomic, weak) id<ScanStringsDataSource> _Nullable stringDataSource;
        [NullAllowed, Export("stringDataSource", ArgumentSemantic.Weak)]
        ScanStringsDataSource StringDataSource { get; set; }

        // @property (nonatomic) BOOL allowSkip;
        [Export("allowSkip")]
        bool AllowSkip { get; set; }

        // @property (nonatomic) double errorCorrectionDuration;
        [Export("errorCorrectionDuration")]
        double ErrorCorrectionDuration { get; set; }

        // @property (nonatomic) BOOL includeCardImage;
        [Export("includeCardImage")]
        bool IncludeCardImage { get; set; }

        // @property (nonatomic) BOOL hideBackButtonImage;
        [Export("hideBackButtonImage")]
        bool HideBackButtonImage { get; set; }

        // @property (nonatomic, strong) UIImage * _Nullable backButtonImage;
        [NullAllowed, Export("backButtonImage", ArgumentSemantic.Strong)]
        UIImage BackButtonImage { get; set; }

        // @property (nonatomic, strong) UIColor * _Nullable backButtonColor;
        [NullAllowed, Export("backButtonColor", ArgumentSemantic.Strong)]
        UIColor BackButtonColor { get; set; }

        // @property (nonatomic, strong) UIFont * _Nullable backButtonFont;
        [NullAllowed, Export("backButtonFont", ArgumentSemantic.Strong)]
        UIFont BackButtonFont { get; set; }

        // @property (nonatomic, strong) UIFont * _Nullable scanCardFont;
        [NullAllowed, Export("scanCardFont", ArgumentSemantic.Strong)]
        UIFont ScanCardFont { get; set; }

        // @property (nonatomic, strong) UIFont * _Nullable positionCardFont;
        [NullAllowed, Export("positionCardFont", ArgumentSemantic.Strong)]
        UIFont PositionCardFont { get; set; }

        // @property (nonatomic, strong) UIFont * _Nullable skipButtonFont;
        [NullAllowed, Export("skipButtonFont", ArgumentSemantic.Strong)]
        UIFont SkipButtonFont { get; set; }

        // @property (nonatomic, strong) NSNumber * _Nullable backButtonImageToTextDelta;
        [NullAllowed, Export("backButtonImageToTextDelta", ArgumentSemantic.Strong)]
        NSNumber BackButtonImageToTextDelta { get; set; }

        // +(ScanViewController * _Nullable)createViewControllerWithDelegate:(id<ScanDelegate> _Nullable)delegate __attribute__((warn_unused_result));
        [Static]
        [Export("createViewControllerWithDelegate:")]
        [return: NullAllowed]
        ScanViewController CreateViewControllerWithDelegate([NullAllowed] ScanDelegate @delegate);

        // +(void)configure;
        [Static]
        [Export("configure")]
        void Configure();

        // +(BOOL)isCompatible __attribute__((warn_unused_result));
        [Static]
        [Export("isCompatible")]
        //[Verify(MethodToProperty)]
        bool IsCompatible { get; }

        // +(UIImage * _Nullable)cameraImage __attribute__((warn_unused_result));
        [Static]
        [NullAllowed, Export("cameraImage")]
        //[Verify(MethodToProperty)]
        UIImage CameraImage { get; }

        // -(void)cancelWithCallDelegate:(BOOL)callDelegate;
        [Export("cancelWithCallDelegate:")]
        void CancelWithCallDelegate(bool callDelegate);

        // -(void)viewDidLoad;
        [Export("viewDidLoad")]
        void ViewDidLoad();

        // @property (readonly, nonatomic) BOOL shouldAutorotate;
        [Export("shouldAutorotate")]
        bool ShouldAutorotate { get; }

        // @property (readonly, nonatomic) UIInterfaceOrientationMask supportedInterfaceOrientations;
        [Export("supportedInterfaceOrientations")]
        UIInterfaceOrientationMask SupportedInterfaceOrientations { get; }

        // @property (readonly, nonatomic) UIStatusBarStyle preferredStatusBarStyle;
        [Export("preferredStatusBarStyle")]
        UIStatusBarStyle PreferredStatusBarStyle { get; }

        // -(void)viewWillAppear:(BOOL)animated;
        [Export("viewWillAppear:")]
        void ViewWillAppear(bool animated);

        // -(void)viewWillDisappear:(BOOL)animated;
        [Export("viewWillDisappear:")]
        void ViewWillDisappear(bool animated);

        // -(void)captureOutput:(AVCaptureOutput * _Nonnull)output didOutputSampleBuffer:(CMSampleBufferRef _Nonnull)sampleBuffer fromConnection:(AVCaptureConnection * _Nonnull)connection;
        //[Export("captureOutput:didOutputSampleBuffer:fromConnection:")]
        //unsafe void CaptureOutput(AVCaptureOutput output, CMSampleBufferRef* sampleBuffer, AVCaptureConnection connection);

        // -(instancetype _Nonnull)initWithNibName:(NSString * _Nullable)nibNameOrNil bundle:(NSBundle * _Nullable)nibBundleOrNil __attribute__((objc_designated_initializer));
        [Export("initWithNibName:bundle:")]
        [DesignatedInitializer]
        IntPtr Constructor([NullAllowed] string nibNameOrNil, [NullAllowed] NSBundle nibBundleOrNil);

        // -(instancetype _Nullable)initWithCoder:(NSCoder * _Nonnull)aDecoder __attribute__((objc_designated_initializer));
        //[Export("initWithCoder:")]
        //[DesignatedInitializer]
        //IntPtr Constructor(NSCoder aDecoder);
    }
}
