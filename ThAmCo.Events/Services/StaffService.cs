namespace ThAmCo.Events.Services
{
	using Microsoft.EntityFrameworkCore;
	using ThAmCo.Events.Models;

	/// <summary>
	/// Defines the <see cref="StaffService" />
	/// </summary>
	public class StaffService
	{
		/// <summary>
		/// Defines the _context
		/// </summary>
		private readonly ThAmCo.Events.Data.EventsDbContext _context;

		/// <summary>
		/// Initializes a new instance of the <see cref="StaffService"/> class.
		/// </summary>
		/// <param name="context">The context<see cref="ThAmCo.Events.Data.EventsDbContext"/></param>
		public StaffService(ThAmCo.Events.Data.EventsDbContext context)
		{
			_context = context;
		}

		/// <summary>
		/// The GetAllStaff
		/// </summary>
		/// <returns>The <see cref="Task{List{Staff}}"/></returns>
		public async Task<List<Staff>> GetAllStaff()
		{
			var staffMembers = await _context.Staff
				.Include(s => s.Staffings)
				.ThenInclude(s => s.Event)
				.ToListAsync();

			return staffMembers;
		}

		/// <summary>
		/// The GetStaffMember
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int?"/></param>
		/// <returns>The <see cref="Task{Staff}"/></returns>
		public async Task<Staff> GetStaffMember(int? staffId)
		{
			var staffMember = await _context.Staff
				.Include(e => e.Staffings)
				.ThenInclude(s => s.Event)
				.FirstOrDefaultAsync(s => s.StaffId == staffId);

			return staffMember;
		}

		/// <summary>
		/// The GetAvailableStaff
		/// </summary>
		/// <param name="_event">The _event<see cref="Event"/></param>
		/// <returns>The <see cref="Task{List{Staff}}"/></returns>
		public async Task<List<Staff>> GetAvailableStaff(Event _event)
		{
			List<Staff> AvailableStaff = [];
			var staff                  = await GetAllStaff();

			foreach (var s in staff)
			{
				var staffings = s.Staffings;

				if (staffings.Count == 0)
				{
					AvailableStaff.Add(s);
					continue;
				}

				bool isAvailable = true;
				foreach (var staffing in staffings)
				{
					if (staffing.EventId == _event.EventId || staffing.Event.Date == _event.Date.Date)
					{
						isAvailable = false;
						break;
					}
				}

				if (isAvailable)
				{
					AvailableStaff.Add(s);
				}
			}

			return AvailableStaff;
		}

		/// <summary>
		/// The DeleteStaffMember
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task DeleteStaffMember(int staffId)
		{
			var staffMember = await GetStaffMember(staffId);
			_context.Staff.Remove(staffMember);
			_context.SaveChanges();
		}

		/// <summary>
		/// The GetStaffMemberEvents
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <returns>The <see cref="Task{List{Event}}"/></returns>
		internal async Task<List<Event>> GetStaffMemberEvents(int staffId)
		{
			var staffMember    = await GetStaffMember(staffId);
			List<Event> events = staffMember.Staffings.Select(x => x.Event).ToList();
			return events;
		}

		/// <summary>
		/// The CreateStaffing
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <param name="@event">The event<see cref="Event"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CreateStaffing(int staffId, Event @event)
		{
			if (staffId == 0)
			{
				return;
			}
			var staff = await GetStaffMember(staffId);

			if (staff == null)
			{
				return;
			}
			
			staff.Staffings.Add(new Staffing() { Staff = staff, Event = @event });
			_context.SaveChanges();
		}

		/// <summary>
		/// The CancelStaffing
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CancelStaffing(int staffId, int eventId)
		{
			var staffMember = await GetStaffMember(staffId);
			var staffing    = await GetStaffing(staffId, eventId);

			staffMember.Staffings.Remove(staffing);
			_context.SaveChanges();
		}

		/// <summary>
		/// The GetStaffing
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{Staffing}"/></returns>
		private async Task<Staffing> GetStaffing(int staffId, int eventId)
		{
			var staffings = await GetStaffings(staffId, eventId);
			return staffings.Where(x => x.EventId == eventId).FirstOrDefault();
		}

		/// <summary>
		/// The GetStaffings
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{List{Staffing}}"/></returns>
		private async Task<List<Staffing>> GetStaffings(int staffId, int eventId)
		{
			var staffMember         = await GetStaffMember(staffId);
			List<Staffing> staffing = staffMember.Staffings.Where(s => s.Event.Date >= DateTime.Today && !s.Event.IsCanceled).OrderBy(x => x.Event.Date).ToList();
			return staffing;
		}

		/// <summary>
		/// The GetPastStaffings
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <returns>The <see cref="Task{List{Staffing}}"/></returns>
		private async Task<List<Staffing>> GetPastStaffings(int staffId, int eventId)
		{
			var staffMember         = await GetStaffMember(staffId);
			List<Staffing> staffing = staffMember.Staffings.Where(s => s.Event.Date < DateTime.Today || s.Event.IsCanceled).OrderBy(x => x.Event.Date).ToList();
			return staffing;
		}

		/// <summary>
		/// The CreateStaff
		/// </summary>
		/// <param name="staff">The staff<see cref="Staff"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task CreateStaff(Staff staff)
		{
			_context.Staff.Add(staff);
			await _context.SaveChangesAsync();
		}

		/// <summary>
		/// The UpdateStaffAttendance
		/// </summary>
		/// <param name="staffId">The staffId<see cref="int"/></param>
		/// <param name="eventId">The eventId<see cref="int"/></param>
		/// <param name="didAttend">The didAttend<see cref="bool"/></param>
		/// <returns>The <see cref="Task"/></returns>
		public async Task UpdateStaffAttendance(int staffId, int eventId, bool didAttend)
		{
			var staffing = await GetStaffing(staffId, eventId);

			if (staffing != null)
			{
				staffing.DidAttend = didAttend;
				await _context.SaveChangesAsync();
			}
		}
	}
}
