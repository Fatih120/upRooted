// Avalonia.Base, Version=11.3.12.0, Culture=neutral, PublicKeyToken=c8d484a7012f9a8b
// Avalonia.Utilities.SpringSolver
using System;

internal struct SpringSolver
{
	private readonly double m_w0;

	private readonly double m_zeta;

	private readonly double m_wd;

	private readonly double m_A;

	private readonly double m_B;

	public SpringSolver(double P_0, double P_1, double P_2, double P_3)
		: this(Math.Sqrt(P_1 / P_0), P_2 / (2.0 * Math.Sqrt(P_1 * P_0)), P_3)
	{
	}

	public SpringSolver(double P_0, double P_1, double P_2)
	{
		m_w0 = P_0;
		m_zeta = P_1;
		if (m_zeta < 1.0)
		{
			m_wd = m_w0 * Math.Sqrt(1.0 - m_zeta * m_zeta);
			m_A = 1.0;
			m_B = (m_zeta * m_w0 + (0.0 - P_2)) / m_wd;
		}
		else
		{
			m_A = 1.0;
			m_B = 0.0 - P_2 + m_w0;
			m_wd = 0.0;
		}
	}

	public readonly double Solve(double P_0)
	{
		P_0 = ((!(m_zeta < 1.0)) ? ((m_A + m_B * P_0) * Math.Exp((0.0 - P_0) * m_w0)) : (Math.Exp((0.0 - P_0) * m_zeta * m_w0) * (m_A * Math.Cos(m_wd * P_0) + m_B * Math.Sin(m_wd * P_0))));
		return 1.0 - P_0;
	}
}

