﻿using Forum.Models;

namespace Forum.DAL
{ // På modulen så står det egt " IItemrepository " for I står for Interface. 
    public interface InterListingRepository
    {
        Task<IEnumerable<Listing>> GetAll();
        Task<Listing?>GetListingById(int id);

        Task <bool>Create(Listing listing);

        Task <bool>Update(Listing listing);

        Task<bool> Delete(int id);
    }
}
