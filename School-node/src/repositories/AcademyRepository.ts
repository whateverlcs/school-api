import { prisma } from '../config/prisma';
import { Prisma, Academy } from '@prisma/client';

export class AcademyRepository {
  async create(data: Prisma.AcademyCreateInput): Promise<Academy> {
    const academy = await prisma.academy.create({
      data,
    });
    return academy;
  }

  async update(id: number, data: Prisma.AcademyUpdateInput): Promise<Academy> {
    const academy = await prisma.academy.update({
      where: {
        id,
      },
      data,
    });
    return academy;
  }

  async delete(id: number): Promise<Academy> {
    const academy = await prisma.academy.delete({
      where: {
        id,
      },
    });
    return academy;
  }

  async findById(id: number): Promise<Academy | null> {
    const academy = await prisma.academy.findUnique({
      where: {
        id,
      },
    });
    return academy;
  }

  async findByName(name: string): Promise<Academy[]> {
    const academies = await prisma.academy.findMany({
      where: {
        name,
      },
    });
    return academies;
  }

  async findByState(state: string): Promise<Academy[]> {
    const academies = await prisma.academy.findMany({
      where: {
        state,
      },
    });
    return academies;
  }

  async findByCity(city: string): Promise<Academy[]> {
    const academies = await prisma.academy.findMany({
      where: {
        city,
      },
    });
    return academies;
  }

  async findAll(): Promise<Academy[]> {
    const academies = await prisma.academy.findMany();
    return academies;
  }
}
