using System;
using It_career_project.Data;
using It_career_project.Data.Models;
using System.Linq;

namespace It_career_project.Business
{
    public class GameStudioController
    {
        private VideoGamePlatformContext context;

        public GameStudioController()
        {
            context = new VideoGamePlatformContext();
        }

        public GameStudioController(VideoGamePlatformContext context)
        {
            this.context = context;
        }

        public void AddStudio(string name, bool isUnderContract)
        {
            context
                .GameStudios
                .Add(new GameStudio(name, isUnderContract));

            context.SaveChanges();
        }

        public void EditStudio(string oldStudioName, string newStudioName, bool isUnderContract)
        {
            GameStudio gameStudioToBeEditted = GetStudioByName(oldStudioName);

            if (oldStudioName != newStudioName)
            {
                EditStudioName(oldStudioName, newStudioName);
            }

            bool oldIsUnderContract = gameStudioToBeEditted.UnderContract;

            if (oldIsUnderContract != isUnderContract)
            {
                EditStudioIsUnderContract(newStudioName, isUnderContract);
            }
        }

        public void EditStudioName(string oldName, string newName)
        {
            GameStudio studioToBeChanged = GetStudioByName(oldName);
            GameStudio studioAlreadyExistsCheck =
                context
                .GameStudios
                .FirstOrDefault(x => x.Name == newName);

            if (studioAlreadyExistsCheck != null)
            {
                throw new ArgumentException("A studio with this name already exists!");
            }

            studioToBeChanged.Name = newName;

            context.Update(studioToBeChanged);
            context.SaveChanges();
        }

        public void EditStudioIsUnderContract(string studioName, bool isUnderContract)
        {
            GameStudio gameStudioToBeChanged = GetStudioByName(studioName);
            gameStudioToBeChanged.UnderContract = isUnderContract;

            context.Update(gameStudioToBeChanged);
            context.SaveChanges();
        }

        public GameStudio GetStudioById(int id)
        {
            GameStudio studio =
                context
                .GameStudios
                .FirstOrDefault(x => x.Id == id);

            return studio;
        }

        public GameStudio GetStudioByName(string studioName)
        {
            if (string.IsNullOrEmpty(studioName))
            {
                throw new ArgumentException("Studio name cannot be empty!");
            }

            GameStudio studio =
                context
                .GameStudios
                .FirstOrDefault(x => x.Name == studioName);

            if (studio == null)
            {
                throw new ArgumentException("Invalid game studio!");
            }

            return studio;
        }

        public void ValidateStudioName(string studioName, bool hasToExist = false)
        {
            if (string.IsNullOrEmpty(studioName))
            {
                throw new ArgumentException("Studio name cannot be empty!");
            }

            GameStudio studio = context.GameStudios.ToList().FirstOrDefault(x => x.Name == studioName);

            if (studio != null && !hasToExist)
            {
                throw new ArgumentException("Studio already exists!");
            }
        }
    }
}