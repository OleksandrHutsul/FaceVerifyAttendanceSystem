using FaceVerifyAttendanceSystem.BL.Models;
using FaceVerifyAttendanceSystem.DAL.Entities;
using FaceVerifyAttendanceSystem.DAL.Repositories.Interfaces;
using FaceVerifyAttendanceSystem.DAL.UnitOfWorks.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

public class ScheduleService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ScheduleService> _logger;
    private readonly IRepository<Lesson> _lessonRepository;
    private readonly UserManager<User> _userManager;

    public ScheduleService(IUnitOfWork unitOfWork, ILogger<ScheduleService> logger,
        IRepository<Lesson> lessonRepository, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _lessonRepository = lessonRepository;
        _userManager = userManager;
    }

    public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
    {
        return await _unitOfWork.GetRepository<Schedule>().GetAllWithIncludesAsync(
            s => s.ScheduleDayOfWeek,
            s => s.ScheduleTime,
            s => s.Lesson
        );
    }

    public async Task<Schedule?> GetScheduleByIdAsync(int id)
    {
        var schedules = await _unitOfWork.GetRepository<Schedule>().GetAllWithIncludesAsync(
            s => s.ScheduleDayOfWeek,
            s => s.ScheduleTime,
            s => s.Lesson
        );
        return schedules.FirstOrDefault(s => s.Id == id);
    }

    public async Task AddScheduleAsync(Schedule schedule)
    {
        try
        {
            await _unitOfWork.GetRepository<Schedule>().AddAsync(schedule);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding schedule");
            throw;
        }
    }

    public async Task UpdateScheduleAsync(Schedule schedule)
    {
        try
        {
            await _unitOfWork.GetRepository<Schedule>().UpdateAsync(schedule);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating schedule");
            throw;
        }
    }

    public async Task DeleteScheduleAsync(int id)
    {
        try
        {
            await _unitOfWork.GetRepository<Schedule>().DeleteByIdAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting schedule");
            throw;
        }
    }

    public async Task<IEnumerable<ScheduleDayOfWeek>> GetAllDaysOfWeekAsync()
    {
        var daysOfWeek = await _unitOfWork.GetRepository<ScheduleDayOfWeek>().GetAllAsync();
        if (daysOfWeek == null || !daysOfWeek.Any())
        {
            throw new Exception("No days of the week found.");
        }
        return daysOfWeek;
    }

    public async Task<IEnumerable<ScheduleTime>> GetAllScheduleTimesAsync()
    {
        var scheduleTimes = await _unitOfWork.GetRepository<ScheduleTime>().GetAllAsync();
        if (scheduleTimes == null || !scheduleTimes.Any())
        {
            throw new Exception("No schedule times found.");
        }
        return scheduleTimes;
    }

    public async Task<IEnumerable<Lesson>> GetLessonsByTeacherAsync(int userId)
    {
        return await _unitOfWork.GetRepository<Lesson>().FindAsync(lesson => lesson.UserId == userId);
    }
}