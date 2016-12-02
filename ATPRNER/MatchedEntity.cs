using System;


namespace ATPRNER
{
	public class MatchedEntity
	{
		/// <summary>
		/// Gets or sets the name of the enity.
		/// </summary>
		/// <value>The name of the enity.</value>
		public String enityName {
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
			this.enityName = entityName;
			matchNumber++;
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

