﻿using ImageService.Logging.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServiceGUI.ViewModel
	{
	class LogTuple
		{
		
		private MessageTypeEnum _enumType;
		private string _data;
		public string EnumType
			{
			get { return Enum.GetName(typeof(MessageTypeEnum), _enumType); }
			set { this._enumType = (MessageTypeEnum)Enum.Parse(typeof(MessageTypeEnum), value); }
			}

		public string Data
			{
			get
				{
				return _data;
				}
			set
				{
				_data = value;
				}
			}
		}
	}
