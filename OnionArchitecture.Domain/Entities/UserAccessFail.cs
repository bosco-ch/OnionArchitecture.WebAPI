namespace OnionArchitecture.Domain.Entities
{
    public record UserAccessFail
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User user { get; set; }

        private bool _locked;
        public int AccessFailCount { get; private set; }
        public DateTime? lockedTime { get; private set; }
        private UserAccessFail() { }
        public UserAccessFail(User user)
        {
            this.Id = Guid.NewGuid();
            this.user = user;
        }
        public void Reset()
        {
            this._locked = false;
            this.lockedTime = null;
            this.AccessFailCount = 0;
        }
        public void Fail()
        {
            this.AccessFailCount++;
            if (AccessFailCount >= 3)
            {
                this._locked = true;
                this.lockedTime =
                    DateTime.Now.AddMinutes(5);
            }
        }
        public bool IsLocked()
        {
            if (this._locked)
            {
                if (DateTime.Now >= this.lockedTime)
                {
                    Reset();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
