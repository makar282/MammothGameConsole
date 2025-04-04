using MammothHunting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MammothHunting.Controllers
{
	public interface IMammothMovement
	{
		Pixel CurrentTarget { get; }
		void MoveMammoth(Mammoth mammoth);
	}
}