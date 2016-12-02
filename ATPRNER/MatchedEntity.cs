using System;


namespace ATPRNER
{
	public class MatchedEntity
	{
		/// <summary>
		/// Gets or sets the name of the entity.
		/// </summary>
		/// <value>The name of the entity.</value>
		public String entityName {
			get;
			set;
		}
		/// <summary>
		/// Gets or sets the match number.
		/// </summary>
		/// <value>The match number.</value>
		public int matchNumber {
			get;
			set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ATPRNER.MatchedEntity"/> class.
		/// </summary>
		/// <param name="entityName">Entity name.</param>
		public MatchedEntity (String entityName)
		{
			this.entityName = entityName;
			matchNumber = 1;
		}

		/// <summary>
		/// Increments the match number one unity.
		/// </summary>
		public  void IncrementMatch ()
		{
			matchNumber++;
		}
	}
}

