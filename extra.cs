//<Grid Visibility = "Visible" Background="Transparent" Name="GridHolder" Width="40" Height="40"  VerticalAlignment="Bottom" HorizontalAlignment="Right">
//            <Polygon Points = "0,0 0,20, 20,0" VerticalAlignment="Bottom" HorizontalAlignment="Right" Margin="-1" Stroke="Transparent" Fill="Orange" />
//            <ToggleButton Name = "menu_btn" VerticalAlignment="Bottom" HorizontalAlignment="Left" Width="20" Height="20" Background="White" Padding="0">
//                <Grid>
//                    <Image Source = "menu.png" />
//                    < Popup Name="popup" MouseLeave="Popup_MouseLeave" IsOpen="{Binding IsChecked, ElementName=menu_btn}"   
//                        Placement="Bottom"             
//                        AllowsTransparency="True"  
//                        PopupAnimation="Slide"  
//                        HorizontalOffset="9"  
//                        VerticalOffset="-7">
//                        <Grid Background = "White" >
//                            < ListBox >
//                                < MenuItem Header="Options" Click="MenuItem_Click"/>
//                            </ListBox>
//                        </Grid>
//                    </Popup>
//                </Grid>
//            </ToggleButton>
//            <Button VerticalAlignment = "Top" HorizontalAlignment="Left" Width="20" Height="20" Background="White" Name="sizing_btn" Click="sizing_btn_Click" Padding="0">
//                <Button.Content>
//                    <Image Name = "sizing_btn_image" Source="OpenArrow.png"/>
//                </Button.Content>
//            </Button>
//            <Button VerticalAlignment = "Top" HorizontalAlignment="Right" Width="20" Height="20" Name="exit_btn" Click="exit_btn_Click" Padding="0">
//                <Button.Content>
//                    <Image Source = "Exit.png" />
//                </ Button.Content >
//            </ Button >
//            < Button VerticalAlignment="Top" HorizontalAlignment="Right" Margin="0,-20,0,0" Width="20" Height="20" Name="pin_btn" Click="pin_btn_Click" Padding="0">
//                <Button.Content>
//                    <Image Name = "pin_btn_image" Source="pin.png"/>
//                </Button.Content>
//            </Button>
//        </Grid>