   M S B A M L   `   `   `  ����   BMC.Presentation.POS,    &BMC.Presentation.POS.CAccountingMeters   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     -     �Name$  	AccMeters��	AccMeters��$	��650��$	��843�� � 	Resources    MetersTemplate  PreviousTemplate  CurrentTemplate  ChangeTemplate  ValueTemplate  DeclaredTemplate  VarianceTemplate��%�  &	        &	 �     &	 }    &	 A    &	     &	 �    &	 �    �� DataTemplate_1��.R�� 
Grid_1��$��0��.+��� Border_1��
���   �$��0,0,0,0q�$��0,0,0,0q�$��Stretch=�$��Stretch=��� TextBlock_1������ *
Meter_Desc+�� DataTemplate_2��.R�� 
Grid_2��$��10��.+��� Border_2��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_2������ *Previous_Meter+�� DataTemplate_3��.R�� 
Grid_3��$��10��.+��� Border_3��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_3������ *Current_Meter+�� DataTemplate_4��.R�� 
Grid_4��$��25��.+��� Border_4��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_4��$��Right=�$��0,0,3,0q����� *
Meter_Diff+�� DataTemplate_5��.R�� 
Grid_5��$��10��.+��� Border_5��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_5������ *
declared+�� DataTemplate_6��.R�� 
Grid_6��$��10��.+��� Border_6��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_6��$��Right=�$��0,0,3,0q����� *Meter_Value+�� DataTemplate_7��.R�� 
Grid_7��$��0��.+��� Border_7��
���   �$��-7,-2,-5.5,-2q�$��	0,0,0.5,0q�$��Stretch=�$��Stretch=��� TextBlock_7��$��Right=�$��0,0,3,0q����� *Meter_Variance+.��� 
Grid_8��.+��� Border_8��$��Stretch=�$��Stretch=�$
��Auto��$
��Auto�� �� CornerRadius$ 5,5,5,5�����	#19CDDD98�� -   $��
listMeters��
listMeters��$��	8,146,9,8q�  Bmc_ListViewStyleFixedLock#���  $��Stretch=�$��Segoe UI)� " BMC_ListView_ListViewItemStyle#���   �� SelectionMode$ Single=�����  �� Mode$ Default=� �� View �� 
GridView_1��.P��� GridViewColumn_1�� +	 'CAccountingMeters_xaml_GridViewColumn_1#��� 	  �� Width$	 200�� �� CellTemplate# [  �� GridViewColumn_2�� +
 'CAccountingMeters_xaml_GridViewColumn_2#��� 
 $	 100��# [ �� GridViewColumn_3�� + 'CAccountingMeters_xaml_GridViewColumn_3#���  $	 100��# [ �� GridViewColumn_4�� + 'CAccountingMeters_xaml_GridViewColumn_4#���  $	 100��# [ �� GridViewColumn_5�� + 'CAccountingMeters_xaml_GridViewColumn_5#���  $	 100��# [ �� GridViewColumn_6�� + 'CAccountingMeters_xaml_GridViewColumn_6#���  $	 100��# [ �� GridViewColumn_7�� + 'CAccountingMeters_xaml_GridViewColumn_7#���  $	 100��# [ �� -   $��
textHeader��
textHeader�����	#FFFFFFFF$��Segoe UI)�$
��Bold"�$��18(�$��27��$��245,17,288,0q�$	��Top=� �� TextAlignment$ Center=� % !CAccountingMeters_xaml_textHeader#���  �� Label_1��$
��Left=�$��	20,55,0,0q�$	��Top=�$	��101��$��27.29�� " CAccountingMeters_xaml_Label_1#���    	SegoeUI20#���  $��13(�  FormLabelForegroundColour#���  $
��Bold"�$��Right=��� -   $��lblRecordType��lblRecordType��$��Right=�$��
0,55,600,0q�$	��Top=�$	��101��$��27.29��#���  $��13(�#���  $
��Bold"��� Label_2��$
��Left=�$��	20,80,0,0q�$	��Top=�$	��101��$��27.29�� " CAccountingMeters_xaml_Label_2#���  #���  $��13(�#���  $
��Bold"�$��Right=��� -   $��lblDate��lblDate��$
��Left=�$��
120,80,0,0q�$	��Top=�$	��101��$��27.29��#���  $��13(�#���  $
��Bold"��� -   $��lblType��lblType��$
��Left=�$��
20,105,0,0q�$	��Top=�$	��101��$��27.29�� " CAccountingMeters_xaml_lblType#���  #���  $��13(�#���  $
��Bold"�$��Right=��� -   $��lblTypeValue��lblTypeValue��$
��Left=�$��120,105,0,0q�$	��Top=�$	��101��$��27.29��#���  $��13(�#���  $
��Bold"��� Label_3��$
��Left=�$��
570,55,0,0q�$	��Top=�$	��101��$��27.29�� " CAccountingMeters_xaml_Label_3#���  #���  $��13(�#���  $
��Bold"�$��Right=��� -   $��lblPos��
lblPos��$��Right=�$��	0,55,29,0q�$	��Top=�$	��101��$��27.29��#���  $��13(�#���  $
��Bold"��� Label_4��$
��Left=�$��
570,80,0,0q�$	��Top=�$	��101��$��27.29�� " CAccountingMeters_xaml_Label_4#���  #���  $��13(�#���  $
��Bold"�$��Right=��� -	   $��lblGame��lblGame��$��Right=�$��	0,80,29,0q�$	��Top=�$	��101��$��27.29��#���  $��13(�#���  $
��Bold"��� Label_5��$
��Left=�$��570,105,0,0q�$	��Top=�$	��101��$��27.29�� " CAccountingMeters_xaml_Label_5#���  #���  $��13(�#���  $
��Bold"�$��Right=��� -
   $��lblAsset��lblAsset��$��Right=�$��
0,105,29,0q�$	��Top=�$	��101��$��27.29��#���  $��13(�#���  $
��Bold"��� -   	 ��Name$	 btnExit��btnExit��$��Right=�$	��Top=�$��0,3.339,6.402,0.301q�$��44�� " CAccountingMeters_xaml_btnExit#���  $��1��  BMC_App_Exit#���  
 J� RenderTransformOrigin$
 0.455,0.272&�F� 
Path_1�� �� Stretch$
 Fill=�������}?�3-i��JK�3-i�`$SK�3-i0ZK`4i0ZK�u<i�0ZK�u<i�0ZK@�Di`$SK�Ki�JK�Kiѐ�}?�Ki�q=
ף��@�KiP�n?@�DiP�n?�u<i�P�n?�u<i�P�n?`4iq=
ף��@�3-i��}?�3-i$	��Top=�$��1.998��$��235,40,240.013,0q����� LinearGradientBrush_1�� �� 
StartPoint$ -1.22086e-006,0.499939&� �� EndPoint$ 0.999997,0.499939&�.��� GradientStop_1�� � Color$ 	#FFC4CFBE�� � Offset$ 0Q�� GradientStop_2��$ 	#00C4CFBE��$ 1Q�