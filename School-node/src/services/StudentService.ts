import { StudentRepository } from '../repositories/StudentRepository';
import { Student } from '@prisma/client';
import { CreateStudentDTO, UpdateStudentDTO } from '../dtos/StudentDTO';

export class StudentService {
  constructor(private studentRepository: StudentRepository) {}

  async execute(data: CreateStudentDTO): Promise<Student> {
    const studentAlreadyExists = await this.studentRepository.findByName(data.name);

    if (studentAlreadyExists.length > 0) {
      throw new Error('Student already exists.');
    }

    const student = await this.studentRepository.create(data);

    return student;
  }

  async getById(id: number): Promise<Student> {
    const student = await this.studentRepository.findById(id);

    if (!student) {
      throw new Error('Student not found');
    }

    return student;
  }

  async getAll(): Promise<Student[]> {
    return this.studentRepository.findAll();
  }

  async getByName(name: string): Promise<Student[]> {
    return this.studentRepository.findByName(name);
  }

  async getByAcademy(academyId: number): Promise<Student[]> {
    return this.studentRepository.findByAcademyId(academyId);
  }

  async update(id: number, data: UpdateStudentDTO): Promise<Student> {
    await this.getById(id);
    return this.studentRepository.update(id, data);
  }

  async delete(id: number): Promise<void> {
    await this.getById(id);
    await this.studentRepository.delete(id);
  }
}
