   M S B A M L   `   `   `  ����I  ENetworkService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NetworkServiceNetworkService  5   	   ;"clr-namespace:NetworkService.ViewsNetworkService.Views  5   	   C&clr-namespace:NetworkService.ViewModelNetworkService.ViewModel  5   	       NetworkService.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:NetworkService     5   	   4views"clr-namespace:NetworkService.Views     5   	   <	viewModel&clr-namespace:NetworkService.ViewModel     5   	     � Title$B  <Proizvodnja vetro i solarnih generatora - Isidora Bogdanovic��5
   	   $	��960��6N   $	��640��6[    � 
ResizeMode$ NoResize=�6g    � DataContext 5      2   ,NetworkService.ViewModel.MainWindowViewModel  5   
    � 	Resources 5       �� DataType2   ,NetworkService.ViewModel.GeneratoriViewModel)   #NetworkService.Views.GeneratoriView/   )NetworkService.ViewModel.DisplayViewModel&    NetworkService.Views.DisplayView-   'NetworkService.ViewModel.GraphViewModel$   NetworkService.Views.GraphView��%�   (��       5      *, +)(�� 2     5      *, +)(�� d     5      *, +)�� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      �� 5   
   "  6   .R�5        5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   ]� $��0��6   $��5q�6!   .+�5      o� $	��Top=�6   .@�5       m� $
��Home��6    m� CommandParameter$
 Home��5!       m� Command 5    )   �� *
NavCommand+5!      m� 5"      $��Display Data��6   $ Display Data��5#       5"   1   �� *
NavCommand+5#      m� 5$      $��
Graph Data��6   $ 
Graph Data��5%       5$   /   �� *
NavCommand+5%      5&      5'      � 5(   
   $��1��6   .+�5)      �� $��3��6B   ��6   �� *CurrentViewModel+6B   �� 5*       �� Orientation$ 
Horizontal=�6   $��	0,845,0,0q�62   $��5��6E   $��4��6R   .+�5+      �� $��50��6   $��50��6$   
������ $��0q�6I   ��6]   �� *
BackToHome+.��5,      �� $��Images\Home.png��6   5-      �� 5.      $��50��6   $��50��6$   $��	525,0,0,0q�60   $��0q�6C   
������ ��6p   �� *UndoCommand+.��5/      �� $��Images\Undo.png��6   50      51      52      53      54      