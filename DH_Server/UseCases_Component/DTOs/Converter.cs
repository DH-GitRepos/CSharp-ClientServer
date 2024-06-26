﻿using Entities;

namespace UseCases.DTOs
{
    public interface Converter<D, E> where D : IDto where E : IEntity
    {
        E ConvertDtoToEntity(D dto);
        D ConvertEntityToDto(E entity);
    }
}