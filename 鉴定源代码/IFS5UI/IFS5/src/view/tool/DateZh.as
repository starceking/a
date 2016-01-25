package view.tool
{
	import mx.controls.DateField;
	public class DateZh extends DateField
	{		
		private const MONTH_NAMES:Array = ["一月","二月","三月","四月","五月","六月","七月","八月","九月","十月","十一月","十二月"];
		private const DAY_NAMES:Array = ["七","一","二","三","四","五","六"];
		private const FORMAT_STRING:String = "YYYY-MM-DD";
		
		public function DateZh()
		{
			super();
			this.dayNames=DAY_NAMES;
			this.monthNames=MONTH_NAMES;
			this.formatString=FORMAT_STRING;
			this.yearNavigationEnabled=true;
		}
	}
}