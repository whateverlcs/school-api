import { prisma } from '../config/prisma';
import { Prisma, Student } from '@prisma/client';

export class StudentRepository {
  async create(data: Prisma.StudentUncheckedCreateInput): Promise<Student> {
    const student = await prisma.student.create({
      data,
    });
    return student;
  }

  async update(id: number, data: Prisma.StudentUncheckedUpdateInput): Promise<Student> {
    const student = await prisma.student.update({
      where: {
        id,
      },
      data,
    });
    return student;
  }

  async delete(id: number): Promise<Student> {
    const student = await prisma.student.delete({
      where: {
        id,
      },
    });
    return student;
  }

  async findById(id: number): Promise<Student | null> {
    const student = await prisma.student.findUnique({
      where: {
        id,
      },
    });
    return student;
  }

  async findByName(name: string): Promise<Student[]> {
    const students = await prisma.student.findMany({
      where: {
        name: {
          contains: name,
          mode: 'insensitive',
        },
      },
    });
    return students;
  }

  async findByAcademyId(academyId: number): Promise<Student[]> {
    const students = await prisma.student.findMany({
      where: {
        academyId,
      },
    });
    return students;
  }

  async findAll(): Promise<Student[]> {
    const students = await prisma.student.findMany();
    return students;
  }
}
