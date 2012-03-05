﻿using System;
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
        private const int WHEEL_DELTA = 15;

        delegate void KeyHandler(object source, KeyEventArgs arg);
        bool holding;
        bool scrolling;
        bool watching;
        bool click_watch;
        DateTime click_time;
        bool rightclick_watch;
        DateTime rightclick_time;
        private float CURR_SEN = 10F;

        Vector victor, mrKalman;
        System.Drawing.Point last,lastEst,memoryPoint;
        Kalman kf;
        SyntheticData kfData;

        public MouseDriver()
        {
            watching = holding = scrolling = click_watch = rightclick_watch = false;
            memoryPoint = new System.Drawing.Point(); 

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
            if (touchPoints.Count < 2 && holding)
            {
                mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, dwExtraInfo);
                holding = false;
            }
            if (click_watch)
            {
                if ((DateTime.Now - click_time).Milliseconds > 500 || touchPoints.Count > 1 || pointDist(memoryPoint, Cursor.Position) > 20)
                {
                    memoryPoint = Cursor.Position;
                    click_watch = false;
                }
                else if (!watching)
                {
                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, dwExtraInfo);
                    click_watch = rightclick_watch = false;
                }
            }
            else if (rightclick_watch)
            {
                if (Math.Abs(memoryPoint.Y - Cursor.Position.Y) > 15 || touchPoints.Count != 2) // Figure out whether they're scrolling or not
                {
                    scrolling = true;
                    rightclick_watch = false;
                } 
                if (!watching)
                {
                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, Cursor.Position.X, Cursor.Position.Y, 0, dwExtraInfo);
                    click_watch = rightclick_watch = false;
                }
            }

            int state = UpdateVectors(watching, touchPoints);
            if (touchPoints.Count == 3 && !holding && !scrolling)
            {
                holding = true;
                if (rightclick_watch)
                    rightclick_watch = false;
                mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, dwExtraInfo);
            }
            if (touchPoints.Count < 2 && scrolling)
            {
                scrolling = false;
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

        private int pointDist(System.Drawing.Point point1, System.Drawing.Point point2)
        {
            return Math.Abs(point1.X - point2.X) + Math.Abs(point1.Y - point2.Y);
        }

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
                float distanceA = 0; // get the closest point to the last used point (sticky)
                float distanceB = -1;
                int fingerToUse = 0;
                int index = 0;
                bool stopped = false;
                if (touchPoints.Count > 1)
                    foreach (System.Drawing.Point newpoint in touchPoints)
                    {
                        distanceA = pointDist(newpoint, last);
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
                    if (last.X == 0 && last.Y == 0) // first run finger down
                    {
                        victor.X = victor.Y = 0;
                        if (!click_watch) // click if finger might not already be clicking
                        {
                            memoryPoint = Cursor.Position;
                            click_watch = rightclick_watch = true;
                            click_time = rightclick_time = DateTime.Now;
                        }
                    }
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
                    else if (mrKalman.Y != 0)
                            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (int)(WHEEL_DELTA * mrKalman.Y * CURR_SEN), (UIntPtr)0);
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
            CURR_SEN = (float)mrKalman.Length*1.5F;
            if (CURR_SEN < 5 && CURR_SEN != 0)
                CURR_SEN = 3;
            kfData.GoToNextState();
        }
    }
}
