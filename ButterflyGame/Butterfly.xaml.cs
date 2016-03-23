using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ButterflyGame
{
    public sealed partial class Butterfly : UserControl
    {
        // animate butterfly
        private DispatcherTimer timer;
        // offset to show
        private int currentFrame = 0;
        private int direction = 1;
        private int frameHeight = 132;
        // location
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        // speed
        private readonly double MaxSpeed = 10.0;
        private readonly double Accelerate = 0.5;
        private double speed;
        // angle
        private double Angle = 0;
        private double AngleStep = 5;

        public Butterfly()
        {
            this.InitializeComponent();

            Animate();
        }

        // animate butterfly
        private void Animate()
        {
            // timer_tick will be called in every 125ms
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0,0,0,0,125);
            timer.Start();
        }

        private void Timer_Tick(object sender, object e)
        {
            // current frame 0->4, 4->0, 0->4...
            if (direction == 1) currentFrame++;
            else currentFrame--;
            if (currentFrame == 0 || currentFrame == 4) direction *= -1; // 1 tai -1
            // offset
            SpriteSheetOffset.Y = currentFrame * -frameHeight;
        }

        // show butterfly in Canvas (right location)
        public void SetLocation()
        {
            SetValue(Canvas.LeftProperty, LocationX);
            SetValue(Canvas.TopProperty, LocationY);
        }

        // move
        public void Move()
        {
            // more speed
            speed += Accelerate; // 0 -> 0.5 -> 1 -> 1.5...
            if (speed > MaxSpeed) speed = MaxSpeed; // max 10
            // update location with angle and speed
            LocationX -= (Math.Cos(Math.PI / 180 * (Angle + 90))) * speed;
            LocationY -= (Math.Sin(Math.PI / 180 * (Angle + 90))) * speed;

        }

        // rotate
        public void Rotate(int angleDirection) // 1 or -1
        {
            Angle += AngleStep * angleDirection; // 5 or -5
            ButterflyRotateAngle.Angle = Angle;
        }
    }
}
