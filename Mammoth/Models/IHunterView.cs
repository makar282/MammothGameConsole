using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MammothHunting.Models
{
	public interface IHunterView
	{
		void Clear(Hunter hunter);
		void Draw(Hunter hunter);
	}
}