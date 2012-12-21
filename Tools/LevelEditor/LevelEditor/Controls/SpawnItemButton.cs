using LevelData;
using LevelData.SpawnItems;
using LevelEditor.Forms;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace LevelEditor.Controls
{
    public class SpawnItemButton : Button
    {
        private bool _isSelected = false;
        private Func<string, Image> _getTypeImageByFileName;
        private Func<string, Image> _getAttachImageByFileName;

        public SpawnItem Item
        {
            get;
            private set;
        }

        public bool IsSelected
        {
            get 
            { 
                return _isSelected; 
            }

            set
            {
                if (_isSelected == value)
                    return;

                _isSelected = value;
                Refresh();
            }
        }

        public SpawnItemButton(SpawnItem item, Func<string, Image> getTypeImageByFileName, Func<string, Image> getAttachImageByFileName)
            : base()
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (getTypeImageByFileName == null)
                throw new ArgumentNullException("getTypeImageByFileName");

            if (getAttachImageByFileName == null)
                throw new ArgumentNullException("getAttachImageByFileName");

            _getTypeImageByFileName = getTypeImageByFileName;
            _getAttachImageByFileName = getAttachImageByFileName;
            Item = item;
            FlatStyle = FlatStyle.Flat;
            Size = new Size(PackForm.PixelInSec, 75);
        }

        public override void NotifyDefault(bool value)
        {
            // запрещаем фокусировку, чтобы не рисовалась рамка фокуса по умолчанию, вместо этого будет использоваться IsSelected
            base.NotifyDefault(false);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            Graphics gr = e.Graphics;
            const int SelectionPenWidth = 2;

            if (Parent != null)
            {
                int count = Parent.Controls.OfType<SpawnItemButton>().Where(btn => btn.Location.X == Location.X).Count();

                // если указан Delay == 0 или дробное значение, несколько кнопок могут накладываться друг на друга.
                // Подписываем такие случаи
                if (count > 1)
                {
                    string text = String.Format("({0})", count);
                    var size = gr.MeasureString(text, Font);
                    gr.DrawString(text, Font, Brushes.Red, new RectangleF(SelectionPenWidth, Height - SelectionPenWidth - size.Height, size.Width, size.Height));
                }
            }

            // рисуем рамку для выделенной кнопки            
            if (IsSelected)                
                gr.DrawRectangle(new Pen(Color.Red, SelectionPenWidth), 0, 0, Width, Height);

            int startX = SelectionPenWidth;
            int y = SelectionPenWidth;

            string imgFileName = Pack.SpawnItemConfig.Types[Item.Type];
            Image img = _getTypeImageByFileName(imgFileName);

            // рисуем иконку SpawnItem
            gr.DrawImage(img, startX, y);

            startX += img.Width;
            int x = startX;

            const int AttachImgWidth = 16;
            const int AttachImgHeight = 16;

            // рисуем иконки SpawnItemAttachment. Соблюдаем порядок: сначала по порядку типов, затем по индексу.
            string[] types = Pack.SpawnItemConfig.AttachmentTypes.Keys.ToArray();
            foreach (var attach in 
                Item.Attachments.Select((Attach, Index) =>
                    new { Attach, Index, TypeIndex = Array.IndexOf(types, Attach.Type) })
                        .OrderBy(att => att.TypeIndex).ThenBy(att => att.Index).Select(att => att.Attach))
            {
                imgFileName = Pack.SpawnItemConfig.AttachmentTypes[attach.Type][attach.Name];
                img = _getAttachImageByFileName(imgFileName);
                gr.DrawImage(img, x, y, AttachImgWidth, AttachImgHeight);

                if (x + (2 * AttachImgWidth) > Width - SelectionPenWidth)
                {
                    // переход на следующую строку, если ещё одна картинка не помещается справа
                    x = startX;
                    y += AttachImgHeight;
                }
                else
                {
                    x += AttachImgWidth;
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _getTypeImageByFileName = null;
            _getAttachImageByFileName = null;
        }
    }
}