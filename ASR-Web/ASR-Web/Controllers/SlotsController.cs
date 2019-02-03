using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asr.Data;
using ASR_Web.Data;
using ASR_Web.Models;
using ASR_Web.Models.SlotViewModels;
using ASR_Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASR_Web.Controllers
{
    public class SlotsController : Controller
    {
        private ISlotRepository _repo;
        private IRoomRepository _roomRepo;
        private IStaffRepository _staffRepo;
        private IStudentRepository _studentRepo;
        private ApplicationDbContext _db;

        public SlotsController(ApplicationDbContext db, ISlotRepository repository, IRoomRepository roomRepository, IStaffRepository staffRepository, IStudentRepository studentRepository)
        {
            _db = db;
            _repo = repository;
            _roomRepo = roomRepository;
            _staffRepo = staffRepository;
            _studentRepo = studentRepository;
        }

        //public IActionResult Index()
        //{


        //    return View(_repo.All());
        //}
        
        // GET: Slots
        public IActionResult Index(string RoomSelect, string StaffSelect, string StudentSelect)
        {
            return View(new SlotIndexViewModel
            {
                Slots = _repo.GetFilteredSlots(RoomSelect, StaffSelect, StudentSelect),
                Rooms = new SelectList(_repo.GetDistinctRooms()),
                Staff = new SelectList(_repo.GetDistinctStaff()),
                Students = new SelectList(_repo.GetDistinctStudents()),
            });
        }

        public IActionResult Details(string RoomID, string StartTime)
        {
            //Incoming StartTimes should be in the following format YYYY-MM-DD-THH:mm:ss 2019-01-30T00:00:00

            //Check the RoomID & StartTime fields are there
            if ((string.IsNullOrEmpty(RoomID)) && (string.IsNullOrEmpty(StartTime)))
            {
                return NotFound();
            }

            //Try get the DateTime from the input string StartTime
            if (!(DateTime.TryParse(StartTime, out DateTime startTimeValue)))
            {
                return NotFound();
            }

            //Find the selected slot from the repo/db
            var selectedSlot = _repo.Find(RoomID, startTimeValue);

            //if null then nothing was found
            if (selectedSlot == null)
            {
                return NotFound();
            }

            return View(selectedSlot);
        }

        // GET: Slots/Create
        public IActionResult Create()
        {
            return View(new SlotCreateViewModel
            {
                Slot = new Slot(),
                Result = "",
            });
            //return View();
        }

        // POST: Slots/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Slot slot)
        {
            //Check model is valid
            if (ModelState.IsValid)
            {
                //Validate slot via business rules
                var newSlot = ValidateCreateSlot(slot.RoomID, slot.StartTime, slot.StaffID);
                if (newSlot != null)
                {
                    _repo.Create(slot);
                    return View(new SlotCreateViewModel
                    {
                        Slot = new Slot(),
                        Result = "Slot created successfuly",
                    });
                }
                return View(new SlotCreateViewModel
                {
                    Slot = slot,
                    Result = "Failed to create slot: Breaks business rules",
                });
            }
            return View(new SlotCreateViewModel
            {
                Slot = slot,
                Result = "Failed to create slot: Model invalid",
            });
        }

        // GET: Slots/Book https://localhost:44300/Slots/Book?RoomID=A&StartTime=2019-02-20T10%3A00%3A00
        public IActionResult Book(string RoomID, String StartTime, String BookingID)
        {
            //Check the RoomID & StartTime fields are there
            if ((string.IsNullOrEmpty(RoomID)) && (string.IsNullOrEmpty(StartTime)))
            {
                return NotFound();
            }

            //Try get the DateTime from the input string StartTime
            if (!(DateTime.TryParse(StartTime, out DateTime startTimeValue)))
            {
                return NotFound();
            }

            //Find the selected slot from the repo/db
            var selectedSlot = _repo.Find(RoomID, startTimeValue);

            //if null then nothing was found
            if (selectedSlot == null)
            {
                return NotFound();
            }
            else
            {
                var validSlot = ValidateBookSlot(selectedSlot.RoomID, selectedSlot.StartTime, selectedSlot.StudentID);
                if (validSlot != null)
                {
                    _repo.Book(selectedSlot, BookingID);
                    return View("~/Views/Slots/SuccessfulBooking.cshtml");
                }
                
            }
            return NotFound();
        }

        // GET: Slots/Unbook
        public IActionResult Unbook(string RoomID, String StartTime)
        {
            //Check the RoomID & StartTime fields are there
            if ((string.IsNullOrEmpty(RoomID)) && (string.IsNullOrEmpty(StartTime)))
            {
                return NotFound();
            }

            //Try get the DateTime from the input string StartTime
            if (!(DateTime.TryParse(StartTime, out DateTime startTimeValue)))
            {
                return NotFound();
            }

            //Find the selected slot from the repo/db
            var selectedSlot = _repo.Find(RoomID, startTimeValue);

            //if null then nothing was found
            if (selectedSlot == null)
            {
                return NotFound();
            }
            else
            {
                _repo.Unbook(selectedSlot);
            }
            return View("~/Views/Slots/SuccessfulUnbooking.cshtml");
        }

        // GET: Slots/Delete
        public IActionResult Delete(string RoomID, String StartTime)
        {
            //Check the RoomID & StartTime fields are there
            if ((string.IsNullOrEmpty(RoomID)) && (string.IsNullOrEmpty(StartTime)))
            {
                return NotFound();
            }

            //Try get the DateTime from the input string StartTime
            if (!(DateTime.TryParse(StartTime, out DateTime startTimeValue)))
            {
                return NotFound();
            }

            //Find the selected slot from the repo/db
            var selectedSlot = _repo.Find(RoomID, startTimeValue);

            //if null then nothing was found
            if (selectedSlot == null)
            {
                return NotFound();
            }
            else
            {
                _repo.Delete(selectedSlot);
            }
            return View("~/Views/Slots/SuccessfulBooking.cshtml");
        }

        private Slot ValidateBookSlot(string inputRoom, DateTime inputDate, string inputStaffID, string inputStudentID)
        {

            return null;
        }


        //VALIDATE SLOT START
        private Slot ValidateCreateSlot(string inputRoom, DateTime inputDate, string inputStaffID)
        {
            DateTime returnDate;
            DateTime? returnDateNullable;

            //VALIDATE THE ROOM
            if (_roomRepo.Find(inputRoom) == null)
            {
                //Console.WriteLine("Unable to create slot: Invalid Room");
                return null;
            }

            //VALIDATE THE DATE + TIME
            returnDateNullable = Slot.ValidateDate(inputDate);
            if (!(returnDateNullable.HasValue))
            {
                return null;
            }
            else
            {
                //Cast nullable to normal
                returnDate = (DateTime)returnDateNullable;
            }

            //VALIDATE THE STAFF
            if (_staffRepo.Find(inputStaffID) == null)
            {
                //Console.WriteLine("Unable to create slot: Invalid StaffID");
                return null;
            }

            //VALIDATE THE BOOKING COUNT
            if (_repo.SearchByStaffDate(inputStaffID, returnDate).Count() >= Constants.STAFF_DAILY_BOOKING_LIMIT)
            {
                //Console.WriteLine("Unable to create slot: StaffID has to many bookings");
                return null;
            }

            //VALIDATE THE BOOKING COUNT
            if (_repo.GetRoomSlotsGivenDay(inputRoom, returnDate).Count() >= Constants.ROOM_DAILY_BOOKING_LIMIT)
            {
                //Console.WriteLine("Unable to create slot: Room has to many bookings");
                return null;
            }

            //Search for a matching slot
            if (_repo.Find(inputRoom, returnDate) != null)
            {
                //Console.WriteLine("Unable to create slot: Matching slot already exists");
                return null;
            }

            //TO GET HERE EVERYTHING ELSE PASSED

            //CREATE NEW SLOT
            var returnSlot = new Slot();
            returnSlot.RoomID = inputRoom;
            returnSlot.StartTime = returnDate;
            returnSlot.StaffID = inputStaffID;

            return returnSlot;
        }
        //VALIDATE SLOT END


        //BOOK SLOT START
        public Slot ValidateBookSlot(string inputRoom, DateTime inputDate, string inputStudentID)
        {
            DateTime returnDate;
            DateTime? returnDateNullable;

            //VALIDATE THE ROOM
            if (_roomRepo.Find(inputRoom) == null)
            {
                //Console.WriteLine("Unable to create slot: Invalid Room");
                return null;
            }

            //VALIDATE THE DATE + TIME
            returnDateNullable = Slot.ValidateDate(inputDate);
            if (!(returnDateNullable.HasValue))
            {
                return null;
            }
            else
            {
                //Cast nullable to normal
                returnDate = (DateTime)returnDateNullable;
            }

            //VALIDATE THE STUDENT
            if (_studentRepo.Find(inputStudentID) == null)
            {
                //Console.WriteLine("Unable to book slot: Invalid StudentID");
                return null;
            }

            //VALIDATE THE STUDENT BOOKING COUNT
            if (_repo.SearchByStudentDate(inputStudentID, returnDate).Count() >= Constants.STUDENT_DAILY_BOOKING_LIMIT)
            {
                //Console.WriteLine("Unable to book slot: StudentID has to many bookings");
                return null;
            }

            //Search for a matching slot
            //if slotResult is null it means no match was found - or there was to many matches
            var returnSlot = _repo.Find(inputRoom, returnDate);
            if (returnSlot == null)
            {
                //Console.WriteLine("Unable to book slot: slot does not exists");
                return null;
            }

            if (returnSlot.StudentID != null)
            {
                //Console.WriteLine("Unable to book slot: slot already has a booking");
                return null;
            }

            returnSlot.StudentID = inputStudentID;

            //TO GET HERE EVERYTHING ELSE PASSED

            return returnSlot;
        }

        //BOOK SLOT END

    }



}

   