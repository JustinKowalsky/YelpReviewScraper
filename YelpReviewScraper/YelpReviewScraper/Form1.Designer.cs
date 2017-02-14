namespace YelpReviewScraper
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.myProduct = new System.Windows.Forms.TextBox();
            this.myLocation = new System.Windows.Forms.TextBox();
            this.startSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // myProduct
            // 
            this.myProduct.Location = new System.Drawing.Point(12, 12);
            this.myProduct.Name = "myProduct";
            this.myProduct.Size = new System.Drawing.Size(100, 20);
            this.myProduct.TabIndex = 0;
            // 
            // myLocation
            // 
            this.myLocation.Location = new System.Drawing.Point(178, 12);
            this.myLocation.Name = "myLocation";
            this.myLocation.Size = new System.Drawing.Size(100, 20);
            this.myLocation.TabIndex = 1;
            // 
            // startSearch
            // 
            this.startSearch.Location = new System.Drawing.Point(115, 75);
            this.startSearch.Name = "startSearch";
            this.startSearch.Size = new System.Drawing.Size(75, 23);
            this.startSearch.TabIndex = 2;
            this.startSearch.Text = "Search";
            this.startSearch.UseVisualStyleBackColor = true;
            this.startSearch.Click += new System.EventHandler(this.startSearch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(290, 261);
            this.Controls.Add(this.startSearch);
            this.Controls.Add(this.myLocation);
            this.Controls.Add(this.myProduct);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox myProduct;
        private System.Windows.Forms.TextBox myLocation;
        private System.Windows.Forms.Button startSearch;
    }
}

