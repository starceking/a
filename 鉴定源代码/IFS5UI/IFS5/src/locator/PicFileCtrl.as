package locator
{
	//Import
	import util.Server;
	
	import flash.events.DataEvent;
	import flash.events.Event;
	import flash.events.ProgressEvent;
	import flash.net.FileFilter;
	import flash.net.FileReference;
	import flash.net.URLRequest;
	import flash.net.navigateToURL;
	
	import mx.controls.ProgressBar;
	
	public class PicFileCtrl extends FileReference
	{
		public function PicFileCtrl(prgArg:ProgressBar,psbnameArg:String)
		{
			this.prg=prgArg;
			this.psbname=psbnameArg;
			
			this.addEventListener(Event.SELECT,onSelect);
			this.addEventListener(ProgressEvent.PROGRESS,onProgress);
			this.addEventListener(DataEvent.UPLOAD_COMPLETE_DATA,onUploadComplete);
			super();
		}
		//Object
		var prg:ProgressBar;
		public static var filter:FileFilter=new FileFilter("jpg图片文件","*.jpg");
		//Current Values
		public var psbname:String="";
		public var filename:String="";
		//Handler
		function onSelect(e:Event):void
		{
			prg.visible=true;			
			var uploadURL:URLRequest=new URLRequest();
			uploadURL.url=Server.getMachineImgUrlDownLoad(psbname,filename);			
			prg.setProgress(0,this.size);
			this.upload(uploadURL);
			prg.label="上传进度";
		}		
		function onProgress(e:ProgressEvent):void
		{
			prg.setProgress(e.bytesLoaded,e.bytesTotal);
			if(e.bytesLoaded==e.bytesTotal)
			{
				prg.label="等待完成...";
			}
		}		
		function onUploadComplete(e:DataEvent):void
		{
			prg.label="上传完成，需重新打开页面才能看到效果";
		}
		//External call
		public function downloadWord():void
		{
			navigateToURL(new URLRequest(Server.getMachineImgUrl(psbname,filename)));
		}
	}
}