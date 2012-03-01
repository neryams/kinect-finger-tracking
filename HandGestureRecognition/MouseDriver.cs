using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Windows.Forms;
using System.Windows;
using System.Runtime.InteropServices;
using MouseKeyboardActivityMonitor;
using MouseKeyboardActivityMonitor.WinApi;

namespace HandGestureRecognition
{
    class MouseDriver
    {
        KeyboardHookListener keyboard;
        UIntPtr dwExtraInfo;
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(int dwFlags, int dx, int dy,
        int dwData, UIntPtr dwExtraInfo);


        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_WHEEL = 0x800;
        private const int WHEEL_DELTA = 100;
        private const float CURR_SEN = 6.5F;

        delegate void KeyHandler(object source, KeyEventArgs arg);
        bool clicking;
        bool scrolling;
        bool watching;

        Vector victor, mrKalman;
        System.Drawing.Point last,lastEst;
        Kalman kf;
        SyntheticData kfData;

        public MouseDriver()
        {
            watching = false;
            clicking = false;
            scrolling = false;

            kfData = new SyntheticData();
            keyboard = new KeyboardHookListener(new GlobalHooker());
            initKalman();

            victor.X = 0;
            victor.Y = 0;
            mrKalman.X = 0;
            mrKalman.Y = 0;
        }

        private void initKalman()
        {
            last = lastEst = new System.Drawing.Point();
            kf = new Kalman(kfData.state,kfData.transitionMatrix, kfData.measurementMatrix, 
                kfData.processNoise, kfData.measurementNoise);
            kf.ErrorCovariancePost = kfData.errorCovariancePost;
        }

        public MouseDriver(/*Seq<PointF> points, */int fingerNum, ArrayList touchPoints) : this()
        {
            AddFrame(/*points, */fingerNum, touchPoints);
        }

        public bool AddFrame(/*Seq<PointF> points, */int fingerNum, ArrayList touchPoints) 
        {
            if (touchPoints.Count >= 1 && !watching)
            {
                watching = true;
            }
            else if (touchPoints.Count < 1 && watching)
            {
                watching = false;
                mrKalman.X = 0;
                mrKalman.Y = 0;
                kfData.state.SetValue(0);
                initKalman();
            }

            int state = UpdateVectors(watching, touchPoints);

            if (touchPoints.Count < 2 )
            {
                if (clicking)
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, dwExtraInfo);
                    clicking = false;
                }
            }
            if (touchPoints.Count != 3)
            {
                scrolling = false;
            }
            if (touchPoints.Count == 2 && !clicking)
            {
                clicking = true;
                mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, dwExtraInfo);
            }
            if (touchPoints.Count == 3)
            {
                if (clicking)
                {
                    mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, dwExtraInfo);
                    clicking = false;
                }
                if (victor.Y > 0)
                {
                    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, WHEEL_DELTA, (UIntPtr)0);
                    scrolling = true;
                }
                if (victor.Y < 0)
                {
                    mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -1 * WHEEL_DELTA, (UIntPtr)0);
                    scrolling = true;
                }
            }
            
            return watching;
        }

        /*private int CheckForMouseClicks(Contour<System.Drawing.Point> movementContour)
        {

            foreach (PointF point in latestPoints)
            {
                avX += (int)point.X / pointsCount;
                avY += (int)point.Y / pointsCount;
            }
            return 0;
        }*/

        private int UpdateVectors(bool watching, ArrayList touchPoints)
        {
            int state = 0;
            if (touchPoints.Count > 0)
            {
                #region process the point history
                /*int index = 0;
                foreach (System.Drawing.Point each in touchPoints) // put the points into the current "frame"
                {
                    pointSet[index, 0, pointSetPointer] = ((System.Drawing.Point)touchPoints[index]).X;
                    pointSet[index, 1, pointSetPointer] = ((System.Drawing.Point)touchPoints[index]).Y;
                    index++;
                }
                if (pointCount < pointSet.GetLength(2) - 1)
                    pointCount++;
                if (pointSetPointer < pointSet.GetLength(2) - 1)
                    pointSetPointer++;
                else
                    pointSetPointer = 1;

                if (pointSetPointer != 0)
                {
                    for (index = pointSetPointer; (index == 0 && pointCount == 1) || index > 0; index--)
                    {
                        SortedList<int,int[]> closests = new SortedList<int, int[]>();
                        for (int c_points = pointSet.GetLength(0) - 1; c_points >= 0; c_points--)
                        {
                            for (int p_points = pointSet.GetLength(0) - 1; p_points >= 0; p_points--)
                            {
                            }
                            System.Drawing.Point currPoint = (System.Drawing.Point)touchPoints[i_points];
                            int dist = 
                            closests.Add(
                        }
                    }
                }*/
                #endregion
                float distanceA = 0;
                float distanceB = -1;
                int fingerToUse = 0;
                int index = 0;
                bool stopped = false;
                if (touchPoints.Count > 1)
                    foreach (System.Drawing.Point newpoint in touchPoints)
                    {
                        distanceA = Math.Abs(newpoint.X - last.X) + Math.Abs(newpoint.Y - last.Y);
                        if (distanceA < distanceB || distanceB < 0)
                        {
                            distanceB = distanceA;
                            fingerToUse = index;
                        }
                        index++;
                    }
                else
                    fingerToUse = 0;

                System.Drawing.Point newp = (System.Drawing.Point)touchPoints[fingerToUse];

                if (watching && !stopped)
                {
                    if (last.X == 0 && last.Y == 0)
                        victor.X = victor.Y = 0;
                    else
                    {
                        victor.X = newp.X - last.X;
                        victor.Y = newp.Y - last.Y;
                    }
                    kfData.state[0, 0] = kfData.state[0, 0] + (float)victor.X;
                    kfData.state[1, 0] = kfData.state[1, 0] + (float)victor.Y;

                    Matrix<float> prediction = kf.Predict();
                    PointF predictProint = new PointF(prediction[0, 0], prediction[1, 0]);
                    // The mouse input points.
                    PointF measurePoint = new PointF(kfData.GetMeasurement()[0, 0],
                        kfData.GetMeasurement()[1, 0]);
                    Matrix<float> estimated = kf.Correct(kfData.GetMeasurement());
                    // The resulting point from the Kalman Filter.
                    System.Drawing.Point newpEst = new System.Drawing.Point((int)estimated[0, 0], (int)estimated[1, 0]);
                    mrKalman.X = newpEst.X - lastEst.X;
                    mrKalman.Y = newpEst.Y - lastEst.Y;
                    if (!scrolling)
                    {
                        UpdateCursor();
                    }
                    last = newp;
                    lastEst = newpEst;
                }

            }
            return state;
        }

        private void UpdateCursor()
        {
            System.Drawing.Point position = Cursor.Position;
            position.Offset((int)((mrKalman.X) * CURR_SEN), (int)((mrKalman.Y) * CURR_SEN * -1));
            Cursor.Position = position;
            kfData.GoToNextState();
        }
    }
}
