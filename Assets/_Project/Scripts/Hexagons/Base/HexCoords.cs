namespace Game.Hexagons {
	public struct HexCoords : ICoords {
		private readonly int q_;
		private readonly int r_;
		public HexCoords(int q, int r) {
			q_ = q;
			r_ = r;
		}
		public float GetDistance(ICoords other) => (this - (HexCoords)other).AxialLength();
		private int AxialLength() {
			if (q_ == 0 && r_ == 0) return 0;
			if (q_ > 0 && r_ >= 0) return q_ + r_;
			if (q_ <= 0 && r_ > 0) return -q_ < r_ ? r_ : -q_;
			if (q_ < 0) return -q_ - r_;
			return -r_ > q_ ? -r_ : q_;
		}
		public static HexCoords operator -(HexCoords a, HexCoords b) {
			return new HexCoords(a.q_ - b.q_, a.r_ - b.r_);
		}
	}
}
