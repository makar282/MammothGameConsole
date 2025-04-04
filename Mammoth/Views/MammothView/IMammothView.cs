using MammothHunting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MammothHunting.Views
{
	public interface IMammothView
	{
		void Draw(Mammoth mammoth);
		void Clear(Mammoth mammoth);
	}
}