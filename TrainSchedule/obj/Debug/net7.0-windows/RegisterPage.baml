   M S B A M L   `   `   `  ����H  DTrainSchedule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:TrainSchedule.ViewModelsTrainSchedule.ViewModels  5      C&clr-namespace:TrainSchedule.ConvertersTrainSchedule.Converters  5           TrainSchedule.RegisterPage   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      5vm&clr-namespace:TrainSchedule.ViewModels     5      =
converters&clr-namespace:TrainSchedule.Converters     5        N� Title$  Register��5	       � 	Resources 5      8   2TrainSchedule.Converters.BoolToVisibilityConverter   BoolToVisibilityConverter��%   &	          5   
   6/   5       � DataContext 5      4   .TrainSchedule.ViewModels.RegistrationViewModel  5   
   .��5      � .+�5   
   �� 
������� �� CornerRadius$ 15��5       �� Padding$ 30q�5      $	��420��5      $	��520��5      $��Center=�5      $	��Top=�5      $��0,75,0,0q�5       J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         Color$ Black��6#      
BlurRadius$ 20Q�5   #      ShadowDepth$ 10Q�5   #   	   Opacity$		 0.3Q�5    #   ._�5#      �� $
��Auto=�6   $��Disabled=�6>   .��5$      �� $��Center=�6   .+�5&      �� $��Register��6   $��Center=�5'      $��36(�5(      $
��Bold"�5)      ���#1976D2$��0,0,0,20q�5+      �� 5.      $��Email��6   $��20(�6,   $
��Bold"�6:   $��0,10,0,0q�6L   �� 5/      $	��300��6   $��16(�6*   $��0,10,0,0q�68   ��50      �� *
User.Email+
 �� UpdateSourceTrigger$
 PropertyChanged=��� 53      $��Password��6   $��20(�6/   $
��Bold"�6=   $��0,30,0,0q�6O   G� 54      -   5        $	��300��54   "   $��16(�6.   $��0,10,0,0q�6<   55   "   �� 58      $��Confirm Password��6   $��20(�67   $
��Bold"�6E   $��0,30,0,0q�6W   G� 59      -   5        $	��300��59   "   $��16(�6.   $��0,10,0,0q�6<   5:   "   �� 5=      $��
First Name��6   $��20(�61   $
��Bold"�6?   $��0,30,0,0q�6Q   �� 5>      $	��300��6   $��16(�6*   $��0,10,0,0q�68   ��5?      �� *User.FirstName+$
 PropertyChanged=��� 5B      $��	Last Name��6   $��20(�60   $
��Bold"�6>   $��0,30,0,0q�6P   �� 5C      $	��300��6   $��16(�6*   $��0,10,0,0q�68   ��5D      �� *User.LastName+$
 PropertyChanged=��� 5G      $��Phone Number��6   $��20(�63   $
��Bold"�6A   $��0,30,0,0q�6S   �� 5H      $	��300��6   $��16(�6*   $��0,10,0,0q�68   ��5I      �� *User.PhoneNumber+$
 PropertyChanged=��� 5L      $��Ticket Type��6   $��20(�62   $
��Bold"�6@   $��0,30,0,0q�6R   �� 5M      $	��300��6   $��16(�6+   $��0,10,0,0q�69    �� SelectedItem 5N      �� *User.TicketType+$
 PropertyChanged=�.@�5O      �� $��Regular��6'   �� 5P      $��Student��6'   �� 5Q      $��Senior��6'   5R      �� 5U      $��Register��6   $	��300��60   $��50��6<   ���#1976D2
�������$��20(�5V   E   $��0,30,0,0q�5W      ��6/   �� *RegisterCommand+�� 5Z      $��Center=�5[       
���  ��$��14(�5]       $��0,20,0,0q�5^       ��5Z       �� *ErrorMessage+y�5_       �� *ShowErrorMessage+ �� 	Converter# [  �� 5b      $��Center=�6    $��0,20,0,0q�6=   $��16(�6O   �� 5c      $�Already have an account? ��6    �� 5d      -   5        $�Log in here��5d      ���#1976D2 �� TextDecorations$ 	Underline{�5f       � Cursor$
 Hand��5g      5h      5i      5j      5k      5l      5m      5n      