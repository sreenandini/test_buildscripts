   M S B A M L   `   `   `  ����   BMC.Presentation.POS'    !BMC.Presentation.POS.ServiceCalls   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -   Window_1��  � Title$  ServiceCalls��$	��768��$
��1024�����	#66000000$
��Left=�$	��Top=� � AllowsTransparency .  � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar .  .��� 
Grid_1��$	��500��$	��800��   MainScreenBG#���   .+��� TextBlock_1�� �� TextAlignment$ Center=�$��Segoe UI)�$
��Bold"�$��18(�$��422,22,385,0q� J� RenderTransformOrigin$ 0.5,0.5&� ! ServiceCalls_xaml_TextBlock_1#���  ���	#FFFFFFFF$��20��$	��Top=�z�V� TransformGroup_1��.~��� ScaleTransform_1�� �� ScaleX$ 1Q� �� ScaleY$ 1Q��� SkewTransform_1��	 �� AngleX$	 0Q�
 �� AngleY$
 0Q��� RotateTransform_1�� �� Angle$ 0Q�T� TranslateTransform_1�� T� X$ 0Q� T� Y$ 0Q��� -    ��Name$ btnExit��btnExit��$��Right=�$��58��  ServiceCalls_xaml_btnExit#���    BMC_App_Exit#���  $ 0.455,0.272&�$��65��$	��Top=�$��0,11.11,5.548,0q�A� -   $��grdServiceCalls��grdServiceCalls����.  A� Columns$ 7��$	��750��$��12,178,9,33q��� Canvas_1��.+��� -   $��	lblStatus��	lblStatus��  ServiceCalls_xaml_lblStatus#���  $	��Top=�$��Center=�$��23�� �� Left$	 365��
 �� Top$	 605��$	��183��� -   $��progressBar1��progressBar1��$��16��$	��622��$��0,0,0,0q� � Value$	 100Q� � IsIndeterminate .  $	 167��$	 637�� � Triggers :� EventTrigger_1�� :� RoutedEvent$ ProgressBar.Loaded��.[��� BeginStoryboard_1��.���� Storyboard_1��.��W� DoubleAnimation_1�� �� 
TargetName$ progressBar1�� �� TargetProperty$ Value� W� From$ 0X�	 W� To$	 100X� g� Duration$ 0:0:5D��� Canvas_2��$��83.242��$ 12��$ 43.366��$	��778��.+��� TextBlock_3��$
��Auto��$��20��$��13(����	#FFFFFFFF ! ServiceCalls_xaml_TextBlock_3#���  $
��Wrap=�  	SegoeUI20#���  $
��Bold"�$ 6��$ 37.848���� TextBlock_4��$
��Auto��$��20��$��13(����	#FFFFFFFF ! ServiceCalls_xaml_TextBlock_4#���  $
��Wrap=�#���  $
��Bold"�$ 6��$ 7���� TextBlock_5��$
��Auto��$��20��$��13(����	#FFFFFFFF ! ServiceCalls_xaml_TextBlock_5#���  $
��Wrap=�#���  $
��Bold"�$	 326��$ 7���� TextBlock_6��$
��Auto��$��20��$��13(����	#FFFFFFFF !	 ServiceCalls_xaml_TextBlock_6#��� 	 $
��Wrap=�#���  $
��Bold"�$	 326��$ 37.848���� -    ��Name$ lblSerialNo��lblSerialNo��$��50,0,0,0q�$��Segoe UI)�$��13(����	#FFFFFFFF$	��200��$ 68��$ 38���� -   $ lblAsset��lblAsset��$��50,0,0,0q�$��Segoe UI)�$��13(����	#FFFFFFFF$	��200��$ 68��$ 7��$��17���� -   $ lblManufacturer��lblManufacturer��$��50,0,0,0q�$��Segoe UI)�$��13(����	#FFFFFFFF$	��200��$	 390��$ 8��$��17���� -	   $ lblGame��lblGame��$��50,0,0,0q�$��Segoe UI)�$��13(����	#FFFFFFFF$	��250��$	 390��$ 38��� -
    �Name$ 	pnlHeader��	pnlHeader��$��83.242��$��12,14.946,10,0q�$	��Top=�$	��Top=��� -   $ 	txtHeader��	txtHeader��$��	18,20,0,0q�$
 Left=�$��Segoe UI)�$
��Bold"�$��16(�$	��Top=�$
��Left=�$ 0.5,0.5&� .
 *PositionDetailsComp_xaml_btnServiceRequest#��� 
 ���	#FFFFFFFF$��21.28��$
��Auto��F� 
Path_1�� �� Stretch$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$
��Left=�$	��Top=�$��199.975��$��1.998��$��	20,40,0,0q����� LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1��  � Color$  	#FFC4CFBE��! � Offset$! 0Q�� GradientStop_2��$  	#00C4CFBE��$! 1Q�