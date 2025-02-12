using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.ComponentModel;


namespace PrisControlToolkit
{
    public class TitleItemStyle : TableItemStyle, IStateManager
    {
        #region Private Members
        private Color _backColorClosed, _foreColorClosed;
        private int _cellPadding, _cellSpacing;
        #endregion

        public TitleItemStyle()
        {
        }

        // Public Members
        [NotifyParentProperty(true)]
        public Color BackColorClosed
        {
            get { return _backColorClosed; }
            set { _backColorClosed = value; }
        }
        [NotifyParentProperty(true)]
        public Color ForeColorClosed
        {
            get { return _foreColorClosed; }
            set { _foreColorClosed = value; }
        }
        [NotifyParentProperty(true)]
        public int CellPadding
        {
            get { return _cellPadding; }
            set { _cellPadding = value; }
        }
        [NotifyParentProperty(true)]
        public int CellSpacing
        {
            get { return _cellSpacing; }
            set { _cellSpacing = value; }
        }


        #region IStateManager
        bool IStateManager.IsTrackingViewState
        {
            get
            {
                return base.IsTrackingViewState;
            }
        }
        void IStateManager.TrackViewState()
        {
            base.TrackViewState();
        }
        object IStateManager.SaveViewState()
        {
            object[] state = new object[2];
            state[0] = base.SaveViewState();

            object[] extraData = new object[4];
            extraData[0] = _backColorClosed;
            extraData[1] = _foreColorClosed;
            extraData[2] = _cellPadding;
            extraData[3] = _cellSpacing;
            state[1] = (object) extraData;

            return state;
        }
        void IStateManager.LoadViewState(object state)
        {
            if (state == null)
                return;
            object[] myState = (object[]) state;
            base.LoadViewState(myState[0]);

            object[] extraData = (object[])myState[1];
            _backColorClosed = (Color)extraData[0];
            _foreColorClosed = (Color)extraData[1];
            _cellPadding = (int)extraData[2];
            _cellSpacing = (int)extraData[3];
        }
        #endregion
    }
}
