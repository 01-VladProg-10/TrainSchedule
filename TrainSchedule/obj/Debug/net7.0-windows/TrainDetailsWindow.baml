   M S B A M L   `   `   `  ����H  DTrainSchedule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:TrainScheduleTrainSchedule  5   	   &     TrainSchedule.TrainDetailsWindow   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:TrainSchedule     5   	     � Title$  TrainDetailsWindow��5   	   $	��450��6$   $	��800��61    � 	Resources 5	       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property	 P� Value��%�  '��       �� 5
   
   " ��6   .�5      ��  � �� �#1976D26+   �� 5       � ��
 �����6+   �� 5       � ��$ 20,12q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��$ 0q�60   �� 5       � ��$ 40��6'   �� 5       � ��$ 14(�6)   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5      #��z��5      $ 4��5      ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5       5      P� 5       �@J�IsMouseOver$
	 True��61   . �5       ��  � �� �#1565C063   5!      5#      5$      .��5&      � $��20q�6   
 � RowDefinitions
 5'   
   �� 5(      $
��Auto �6   �� 5)      $��* �6   .+�5-   
   �� ���#E3F2FD �� Padding$ 20q�6&   $ 4��63   $��0,0,0,20q�6D   ._�5.      �� .+�5/      �� $��24(�67   $
��Bold"�6E   $��0,0,0,10q�6W   ��6   �� *	TrainName+6W   �� 50      �� 51      $
��Bold"�64   �6   �� *StartStation+64    �� 52      $� → ��6    �� 53      $
��Bold"�65   �6   �� *FinishStation+65   54      �� 55      �� 56      �6   �� *	Departure+ �� 57      $	� - ��6    �� 58      �6   �� *	Arrival+59      �� 5:      $��18(�63   $
��Bold"�6A   $��0,10,0,0q�6S   ��6   �� *Price+6S   5;      5<      �� 5?   
   $��1��6   ��6    �� *StationDetails+��5@      �� 5A      .R�5B      � $	��0,5q�6    � ColumnDefinitions 5C      �� 5D      $��* �6/   �� 5E      $
��Auto �6/   �� 5F      $
��Auto �6/   .+�5H      �� $��Center=�6A   ��6$   �� *StationName+6A   �� 5I      $��1��6$   $��20,0,0,0q�6S   $��Center=�6e   ��64   �� *DepartureTime+6e   �� 5J      $��2��6$   $��20,0,0,0q�6Q   $��Center=�6c   ��64   �� *ArrivalTime+6c   5K      5L      5N      5O      5P      