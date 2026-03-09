import { describe, it, expect, vi } from 'vitest';
import { StudentService } from './StudentService';
import { StudentRepository } from '../repositories/StudentRepository';
import { CreateStudentDTO, Schooling } from '../dtos/StudentDTO';

describe('StudentService', () => {
  const studentRepositoryMock = {
    create: vi.fn(),
    findByName: vi.fn(),
    findAll: vi.fn(),
    findById: vi.fn(),
    findByAcademyId: vi.fn(),
    update: vi.fn(),
    delete: vi.fn(),
  } as unknown as StudentRepository;

  const studentService = new StudentService(studentRepositoryMock);

  it('should be able to create a new student', async () => {
    const studentData: CreateStudentDTO = {
      name: 'John',
      surname: 'Doe',
      email: 'john@doe.com',
      age: 20,
      schooling: Schooling.High_School,
      academyId: 1,
    };

    vi.spyOn(studentRepositoryMock, 'findByName').mockResolvedValue([]);
    vi.spyOn(studentRepositoryMock, 'create').mockResolvedValue({
      id: 1,
      ...studentData,
      createdAt: new Date(),
      updatedAt: new Date(),
    });

    const student = await studentService.execute(studentData);

    expect(student).toHaveProperty('id');
    expect(student.name).toBe(studentData.name);
    expect(studentRepositoryMock.create).toHaveBeenCalledWith(studentData);
  });

  it('should be able to get a student by id', async () => {
    const mockStudent = { 
      id: 1, 
      name: 'John', 
      surname: 'Doe', 
      email: 'john@doe.com', 
      age: 20, 
      schooling: 1, 
      academyId: 1, 
      createdAt: new Date(), 
      updatedAt: new Date() 
    };

    vi.spyOn(studentRepositoryMock, 'findById').mockResolvedValue(mockStudent);

    const student = await studentService.getById(1);

    expect(student.id).toBe(1);
    expect(student.name).toBe('John');
  });

  it('should throw error if student is not found by id', async () => {
    vi.spyOn(studentRepositoryMock, 'findById').mockResolvedValue(null);

    await expect(studentService.getById(999)).rejects.toThrow('Student not found');
  });

  it('should be able to list all students in an academy', async () => {
    const mockStudents = [
      { id: 1, name: 'S1', surname: 'X', email: '@', age: 10, schooling: 0, academyId: 1, createdAt: new Date(), updatedAt: new Date() },
      { id: 2, name: 'S2', surname: 'Y', email: '@', age: 11, schooling: 1, academyId: 1, createdAt: new Date(), updatedAt: new Date() },
    ];

    vi.spyOn(studentRepositoryMock, 'findByAcademyId').mockResolvedValue(mockStudents);

    const students = await studentService.getByAcademy(1);

    expect(students).toHaveLength(2);
    expect(students[0].academyId).toBe(1);
  });

  it('should be able to update a student', async () => {
    const mockStudent = { id: 1, name: 'S1', surname: 'X', email: '@', age: 10, schooling: 0, academyId: 1, createdAt: new Date(), updatedAt: new Date() };

    vi.spyOn(studentRepositoryMock, 'findById').mockResolvedValue(mockStudent);
    vi.spyOn(studentRepositoryMock, 'update').mockResolvedValue({ ...mockStudent, name: 'New Name' });

    const student = await studentService.update(1, { name: 'New Name' });

    expect(student.name).toBe('New Name');
  });

  it('should be able to delete a student', async () => {
    const mockStudent = { id: 1, name: 'S1', surname: 'X', email: '@', age: 10, schooling: 0, academyId: 1, createdAt: new Date(), updatedAt: new Date() };
    
    vi.spyOn(studentRepositoryMock, 'findById').mockResolvedValue(mockStudent);
    vi.spyOn(studentRepositoryMock, 'delete').mockResolvedValue(mockStudent);

    await studentService.delete(1);

    expect(studentRepositoryMock.delete).toHaveBeenCalledWith(1);
  });
});
