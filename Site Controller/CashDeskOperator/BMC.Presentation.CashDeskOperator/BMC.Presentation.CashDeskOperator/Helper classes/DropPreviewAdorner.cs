#region File Description
//-------------------------------------------------------------------------------------
// File: DropPreviewAdorner.cs
//
// Namespace: BMC.Presentation.Helpers
//
// Description: DragDropManager helper class. Based on open source projects from Codeplex, modified
// to suit the BMC Cash Desk Operator
//
// Copyright: Bally Technologies Inc. 2008
//
// Revision History:
//------------------------------------------------------------------------------------
// Date				    Engineer			        Description
// -----------------------------------------------------------------------------------
// 3/11/2008          Balasubramanyam.G        Helper class for drag operations
//------------------------------------------------------------------------------------
#endregion

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace BMC.Presentation.Helpers
{
	public class DropPreviewAdorner : Adorner
	{
		private double _left;
		private ContentPresenter _presenter;
		private double _top;

		public DropPreviewAdorner(UIElement feedbackUI, UIElement adornedElt) : base(adornedElt)
		{
			_presenter = new ContentPresenter();
			_presenter.Content = feedbackUI;
			_presenter.IsHitTestVisible = false;
		}

		public double Left
		{
			get { return _left; }
			set
			{
				_left = value;
				UpdatePosition();
			}
		}

		public double Top
		{
			get { return _top; }
			set
			{
				_top = value;
				UpdatePosition();
			}
		}

		protected override int VisualChildrenCount
		{
			get { return 1; }
		}

		private void UpdatePosition()
		{
			AdornerLayer layer = this.Parent as AdornerLayer;
			if (layer != null)
			{
				layer.Update(AdornedElement);
			}
		}

		protected override Size MeasureOverride(Size constraint)
		{
			_presenter.Measure(constraint);
			return _presenter.DesiredSize;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			_presenter.Arrange(new Rect(finalSize));
			return finalSize;
		}

		protected override Visual GetVisualChild(int index)
		{
			return _presenter;
		}

		public override GeneralTransform GetDesiredTransform(GeneralTransform transform)
		{
			GeneralTransformGroup result = new GeneralTransformGroup();
			result.Children.Add(new TranslateTransform(Left, Top));
			if (Left > 0) this.Visibility = Visibility.Visible;
			result.Children.Add(base.GetDesiredTransform(transform));

			return result;
		}
	}

    public interface IDragSourceAdvisor
    {
        UIElement SourceUI { get; set; }

        DragDropEffects SupportedEffects { get; }

        DataObject GetDataObject(UIElement draggedElt);
        void FinishDrag(UIElement draggedElt, DragDropEffects finalEffects);
        bool IsDraggable(UIElement dragElt);
        UIElement GetTopContainer();
    }

    public interface IDropTargetAdvisor
    {
        UIElement TargetUI { get; set; }

        bool ApplyMouseOffset { get; }
        bool IsValidDataObject(IDataObject obj);
        void OnDropCompleted(IDataObject obj, Point dropPoint);
        UIElement GetVisualFeedback(IDataObject obj);
        UIElement GetTopContainer();
    }
}